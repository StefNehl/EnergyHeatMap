import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"

//Services
import { createMapChart } from "../../services/mapChartService";

//models
import { EnergyStateData } from "../../models/EnergyStateData";
import {User} from "../../models/User"

interface Props{
    currentUser:User;
}

const EHAMMapContainer: React.FC<Props> = ({ currentUser }) =>
{
    const [mapRoot, setMapRoot] = useState<am5.Root>();
    const [map, setMap] = useState<am5map.MapChart>();

    useEffect(() => 
    {
        if(mapRoot === undefined)
        {
            setMapRoot(am5.Root.new("chartdiv"));
        }

        if(map === undefined && mapRoot !== undefined)
        {
            setMap(createMapChart(mapRoot as am5.Root));
        }

    })

    return (
        <div id="outerdiv" className="outerdiv">
            <div id="chartdiv" className="chartdiv" />
        </div>
    );
}

export default EHAMMapContainer