import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"

//Services
import { createMapChart } from "../../../services/mapChartService";

//models
import { CountryDataGroupByDate } from "../../../models/CountryDataGroupByDate"

interface Props{
    selectedCountryData: CountryDataGroupByDate | undefined;
}

const EHAmMapContainer : React.FC<Props> = ({ selectedCountryData }) => 
{
    const [mapRoot, setMapRoot] = useState<am5.Root>();
    const [map, setMap] = useState<am5map.MapChart>();

    useEffect(() => 
    {
        console.log("laod data");
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