import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5xy from "@amcharts/amcharts5/xy"

import { createXYChart } from "../../services/chartService";
import { creatSeriesForChart } from "../../services/chartService";

//import css
import './EHChartContainer.css'

//models
import {CryptoStateData} from './../../models/CryptoStateData';

interface Props{
    data: CryptoStateData[]
}



const EHChartContainer: React.FC<Props> = ({ data }) =>
{
    const [chartRoot, setChartRoot] = useState<am5.Root>();
    const [chart, setChart] = useState<am5xy.XYChart>();

    useEffect(() => 
    {
        if(chartRoot === undefined)
        {
            setChartRoot(am5.Root.new("chartdiv"));
        }

        if(chart === undefined && chartRoot !== undefined)
        {
            setChart(createXYChart(chartRoot as am5.Root));
        }

        if(chart !== undefined && chartRoot !== undefined && data.length !== 0) 
        {          
            chart.series.clear();
            chart.xAxes.clear();
            chart.yAxes.clear();

            data.forEach(d => 
            {
                var series = creatSeriesForChart(chart, chartRoot)
                console.log(d.values.length + " items passed to chart");
                series?.data.setAll(d.values);
                console.log(chart?.series.length + " items loaded in chart")
            });
        }

    }, [data]);



    return (
        <div id="chartdiv" className="chartdiv" />
    )
}

export default EHChartContainer;