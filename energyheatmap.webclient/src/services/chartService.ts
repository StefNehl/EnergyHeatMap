import * as am5 from "@amcharts/amcharts5";
import * as am5xy from "@amcharts/amcharts5/xy"
import am5themes_Animated from "@amcharts/amcharts5/themes/Animated"

export function createXYChart(
    root: am5.Root) : am5xy.XYChart
{
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
        wheelY: "zoomX",
        height:700,
        layout: root.verticalLayout
    }));

    chart.set("scrollbarX", am5.Scrollbar.new(root, {
        orientation: "horizontal"
    }));

    chart.xAxes.push(am5xy.DateAxis.new(root, {
        maxDeviation: 0.1,
        baseInterval: {
            timeUnit: "day",
            count: 1
        },
        renderer: am5xy.AxisRendererX.new(root, {}),
        tooltip: am5.Tooltip.new(root, {})
    }));

    chart.yAxes.push(am5xy.ValueAxis.new(root, {
        maxDeviation: 0.2,
        renderer: am5xy.AxisRendererY.new(root, {})
    }));

    let legend = chart.children.push(am5.Legend.new(root, {}));
    legend.data.setAll(chart.series.values);

    return chart;
}

export function creatSeriesForChart(chart : am5xy.XYChart, 
    root : am5.Root, name:string) : am5xy.LineSeries | undefined
{
    let xAxis = chart.xAxes.getIndex(0);
    let yAxis = chart.yAxes.getIndex(0); 

    if(xAxis === undefined)
        return undefined;

    if(yAxis === undefined)
        return undefined;

    let series = chart.series.push(am5xy.LineSeries.new(root, {
        minBulletDistance: 10,
        name: name,
        connect: true,
        xAxis: xAxis,
        yAxis: yAxis,
        valueYField: "value",
        valueXField: "dateTime",
        legendValueText: "{valueY}",
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
        yAxis: yAxis,
        behavior: "zoomX"
    }));
    cursor.lineY.set("visible", false);

    var legend = chart.children.getIndex(1) as am5.Legend;
    if(legend === undefined)
    {
        legend = chart.children.push(am5.Legend.new(root, {
            centerX: am5.p50,
            x: am5.p50
          }));
    }

    legend.data.setAll(chart.series.values);

    series.appear();
    return series;
}