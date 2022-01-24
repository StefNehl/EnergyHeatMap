import React, { useEffect, useState } from "react";
import EHAmMapContainer from "./MapComponent/EHAmMapContainer"

//Services
import { getCountriesData } from "../../services/httpEnergyStatesService";

//models
import {User} from "../../models/User"

//Styles
import "./EHMapDataContainer.css";
import { CountryData } from "../../models/CountryData";
import { Row } from "react-bootstrap";

interface Props{
    currentUser:User;
}

const EHMapDataContainer: React.FC<Props> = ({ currentUser }) =>
{
    const [selectedCountryData, setSelectedCountryData] = useState<CountryData>();
    const [countryData, setCountryData] = useState<CountryData[]>([]);
    
    let fetchEnergyStates = async () => {
        if(countryData !== undefined)
            return;

        let newData = await getCountriesData(
            currentUser)

        if(newData === null)
            return;
        console.log(newData.length + " countryData items loaded");
        
        setCountryData(newData);

        if(selectedCountryData === undefined)
            setSelectedCountryData(newData[newData.length-1]);
    };

    useEffect(() => 
    {
        setTimeout(() => fetchEnergyStates(), 100); 
    }, [selectedCountryData])

    return (
        <div>
            <Row>
                <EHAmMapContainer selectedCountryData={selectedCountryData}/>
            </Row>
            <Row>
                
            </Row>
        </div>
    );
}

export default EHMapDataContainer