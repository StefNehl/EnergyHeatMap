import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"

//Services
import { createMapChart } from "../../services/mapChartService";
import { getEnergyStatesFilteredWithTypeAsync } from "../../services/httpEnergyStatesService";

//models
import { EnergyStateData } from "../../models/EnergyStateData";
import {User} from "../../models/User"

//Styles
import "./EHAMMapContainer.css";

interface Props{
    currentUser:User;
}

const EHAMMapContainer: React.FC<Props> = ({ currentUser }) =>
{
    const [mapRoot, setMapRoot] = useState<am5.Root>();
    const [map, setMap] = useState<am5map.MapChart>();
    const [selectedCountries, setSelectedCountries] = useState<string[]>(['World']);
    const [selectedEnergyStateValueTypes, setSelectedEnergyStateValueTypes] = useState<string[]>(['HashrateProductionInPercentage']);
    const [selectedEnergyState, setSelectedEnergyState] = useState<EnergyStateData>();
    const [energyData, setEnergyData] = useState<EnergyStateData[]>([]);
    
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

        let fetchEnergyStates = async () => {
            // let newData = await getEnergyStatesFilteredWithTypeAsync(
            //     currentUser, 
            //     selectedCountries, 
            //     selectedEnergyStateValueTypes);
            
            // if(newData === null)
            //     return;
            // console.log(newData.length + " energy items loaded");
            
            // setEnergyData(newData);
            // setSelectedEnergyState(newData[0]);
        };

        setTimeout(() => fetchEnergyStates(), 100); 
        // if(map === undefined && mapRoot !== undefined)
        // {
        //     setMap(createMapChart(mapRoot as am5.Root));
        // }
    })

    return (
        <div id="outerdiv" className="outerdiv">
            <div id="chartdiv" className="chartdiv" />
        </div>
    );
}

export default EHAMMapContainer