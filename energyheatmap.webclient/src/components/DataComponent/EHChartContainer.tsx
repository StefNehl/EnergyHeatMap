import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5xy from "@amcharts/amcharts5/xy"

import { createXYChart } from "../../services/chartService";

//import css
import './EHChartContainer.css'

interface Props{
    data: unknown[]
}

const EHChartContainer: React.FC<Props> = ({ data }) =>
{
    const [chart, setChart] = useState<am5xy.LineSeries>()

    useEffect(() => 
    {
        if(chart === undefined)
        {
            let root = am5.Root.new("chartdiv");
            let currentChart = createXYChart(root);
            setChart(currentChart);
        }

        console.log(data.length + " items passed to chart");
        chart?.data.setAll(data);
        console.log(chart?.data.length + " items loaded in chart")
        console.log(data);
    }, [data]);



    return (
        <div id="chartdiv" className="chartdiv" />
    )
}

export default EHChartContainer;