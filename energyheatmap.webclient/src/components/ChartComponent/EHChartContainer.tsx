import React, { useEffect } from "react";

import * as am5 from "@amcharts/amcharts5";
import * as am5xy from "@amcharts/amcharts5/xy"
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated"

//services 
import { getCryptoCoinStatesAsync } from "../../services/httpService"

//models
import { User } from "../../models/User"

//import css
import './EHChartContainer.css'
import { Container } from "react-bootstrap";


interface Props{
    currentUser: User;
}

const EHChartContainer: React.FC<Props> = ( { currentUser }) => 
{
    useEffect(() => 
    {        
        let root = am5.Root.new("chartdiv");
        root.setThemes([
            am5themes_Animated.new(root)
        ]);

        root.dateFormatter.setAll({
            dateFormat: "yyyy-MM-dd",
            dateFields: ["valueX"]
        });

        let chart = root.container.children.push(am5xy.XYChart.new(root, {
            focusable: true,
            panX: true,
            panY: true,
            wheelX: "panX",
            wheelY: "zoomX"
        }));

        let xAxis = chart.xAxes.push(am5xy.DateAxis.new(root, {
            maxDeviation: 0.1,
            groupData: false,
            baseInterval: {
                timeUnit: "day",
                count: 1
            },
            renderer: am5xy.AxisRendererX.new(root, {

            }),
            tooltip: am5.Tooltip.new(root, {})
        }));

        let yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            maxDeviation: 0.2,
            renderer: am5xy.AxisRendererY.new(root, {})
        }));



        let series = chart.series.push(am5xy.LineSeries.new(root, {
            minBulletDistance: 10,
            connect: false,
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            valueXField: "dateTime",
            tooltip: am5.Tooltip.new(root, {
                pointerOrientation: "horizontal",
                labelText: "{valueY}"
            })
        }));

        series.fills.template.setAll({
            fillOpacity: 0.2,
            visible: true
        });

        series.strokes.template.setAll({
            strokeWidth: 2
        });

        // Set up data processor to parse string dates
        // https://www.amcharts.com/docs/v5/concepts/data/#Pre_processing_data
        series.data.processor = am5.DataProcessor.new(root, {
            dateFormat: "yyyy-MM-dd",
            dateFields: ["dateTime"]
        });



        series.bullets.push(function () {
            let circle = am5.Circle.new(root, {
                radius: 4,
                fill: root.interfaceColors.get("background"),
                stroke: series.get("fill"),
                strokeWidth: 2
            })

            return am5.Bullet.new(root, {
                sprite: circle
            })
            
        });

        let cursor = chart.set("cursor", am5xy.XYCursor.new(root, {
            xAxis: xAxis,
            behavior: "none"
        }));
        cursor.lineY.set("visible", false);

        chart.set("scrollbarX", am5.Scrollbar.new(root, {
            orientation: "horizontal"
        }));

        let fetchData = async () => {
            let data = await getCryptoCoinStatesAsync(currentUser);
            console.log(data);
            series.data.setAll(data as unknown[]);
        };

        setTimeout(() => fetchData(), 1000)
    }, [currentUser])

    return(
        <Container>
            <div id="chartdiv" className="chartdiv"/>
        </Container>
    )

}

export default EHChartContainer; 