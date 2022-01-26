import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"

//Services
import { createMapChart } from "../../../services/mapChartService";

//models
import { CountryData } from "../../../models/CountryData"

interface Props{
    selectedCountryData: CountryData | undefined;
}

const EHAmMapContainer : React.FC<Props> = ({ selectedCountryData }) => 
{
    const [mapRoot, setMapRoot] = useState<am5.Root>();
    const [map, setMap] = useState<am5map.MapChart>();

    useEffect(() => 
    {
        if(mapRoot === undefined)
        {
            setMapRoot(am5.Root.new("chartdiv"));
        }

        if(mapRoot !== undefined && selectedCountryData !== undefined)
        {
            setMap(createMapChart(mapRoot as am5.Root, selectedCountryData));
        }
    }, [mapRoot, map, selectedCountryData])

    return (
        <div id="outerdiv" className="outerdiv">
            <div id="chartdiv" className="chartdiv" />
        </div>
    )
}

export default EHAmMapContainer