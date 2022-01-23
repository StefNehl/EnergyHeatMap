import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5xy from "@amcharts/amcharts5/xy"

import { createXYChart } from "../../services/chartService";
import { creatSeriesForChart } from "../../services/chartService";

//import css
import './EHChartContainer.css'

//models
import {CryptoStateData} from './../../models/CryptoStateData';
import { EnergyStateData } from "../../models/EnergyStateData";

interface Props{
    cryptoData: CryptoStateData[];
    energyData: EnergyStateData[];
}

const EHChartContainer: React.FC<Props> = ({ cryptoData, energyData }) =>
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

        if(chart !== undefined && chartRoot !== undefined) 
        {       
            chart.series.clear();
            if(cryptoData.length !== 0)
            {
                cryptoData.forEach(d => 
                {
                    var series = creatSeriesForChart(chart, chartRoot)
                    console.log(d.values.length + " crypto passed to chart");
                    series?.data.setAll(d.values);
                    console.log(series?.data.length + " crypto loaded in chart")
                });
            }

            if(energyData.length !== 0)
            {
                energyData.forEach(d => 
                {
                    var series = creatSeriesForChart(chart, chartRoot)
                    console.log(d.values.length + " energy passed to chart");
                    series?.data.setAll(d.values);
                    console.log(series?.data.length + " energy loaded in chart")
                });
            }

        }

    }, [chartRoot, chart, cryptoData, energyData]);

    return (
        <div id="outerdiv" className="outerdiv">
            <div id="chartdiv" className="chartdiv" />
        </div>
    )
}

export default EHChartContainer;