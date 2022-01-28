import React, { useEffect, useState } from "react";
import { Row } from "react-bootstrap";
import EHAmMapContainer from "./MapComponent/EHAmMapContainer"
import EHSliderComponent from "./DateSelectionComponent/EHSliderComponent"

//Services
import { getCountriesDataGroupedByDate } from "../../services/httpEnergyStatesService";

//models
import { CountryDataGroupByDate } from "../../models/CountryDataGroupByDate";
import {User} from "../../models/User"

//Styles
import "./EHMapDataContainer.css";

interface Props{
    currentUser:User;
}

const EHMapDataContainer: React.FC<Props> = ({ currentUser }) =>
{
    const [selectedCountryData, setSelectedCountryData] = useState<CountryDataGroupByDate>();
    const [countryData, setCountryData] = useState<CountryDataGroupByDate[]>([]);
    
    let fetchEnergyStates = async () => {
        if(countryData.length !== 0)
            return;

        let newData = await getCountriesDataGroupedByDate(
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
                <EHSliderComponent countryData={countryData} setSelectedCountryData={setSelectedCountryData}/>
            </Row>
        </div>
    );
}

export default EHMapDataContainer