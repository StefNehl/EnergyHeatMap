import React, { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";

//Components
import EHChartFilterContainer from "./EHChartFilterComponent/EHChartFilterContainer"
import EHChartContainer from "./EHChartContainer"

//services 
import { getCryptoCoinStatesFilteredWithTypeAsync } from "../../services/httpCryptoCoinStatesService";
import { getEnergyStatesFilteredWithTypeAsync } from "../../services/httpEnergyStatesService";

//models
import { User } from "../../models/User"
import {CryptoStateData} from './../../models/CryptoStateData';
import { EnergyStateData } from "../../models/EnergyStateData";

//Styles
import "./EHDataContainer.css"

interface Props{
    currentUser: User;
}

const EHDataContainer: React.FC<Props> = ( { currentUser }) => 
{
    const [selectedCryptoCoins, setSelectedCryptoCoins] = useState<string[]>([]);
    const [selectedValueTypes, setSelectedValueTypes] = useState<string[]>([]);
    const [selectedCountries, setSelectedCountries] = useState<string[]>([]);
    const [selectedEnergyStateValueTypes, setSelectedEnergyStateValueTypes] = useState<string[]>([]);
    const [cryptoData, setCryptoData] = useState<CryptoStateData[]>([]);
    const [energyData, setEnergyData] = useState<EnergyStateData[]>([]);


    let fetchCryptoCoinStates = async () => {
        let newData = await getCryptoCoinStatesFilteredWithTypeAsync(
            currentUser, 
            selectedCryptoCoins, 
            selectedValueTypes);
        
        if(newData === null)
            return;
        console.log(newData.length + " crypto items loaded");
        
        setCryptoData(newData);
    };

    let fetchEnergyStates = async () => {
        let newData = await getEnergyStatesFilteredWithTypeAsync(
            currentUser, 
            selectedCountries, 
            selectedEnergyStateValueTypes);
        
        if(newData === null)
            return;
        console.log(newData.length + " energy items loaded");
        
        setEnergyData(newData);
    };

    useEffect(() => 
    {   
        setTimeout(() => fetchCryptoCoinStates(), 100);        
        setTimeout(() => fetchEnergyStates(), 100);    
    }, [selectedCryptoCoins, selectedValueTypes, 
        selectedCountries, selectedEnergyStateValueTypes])

    let setCryptoCoinsForFilter = (coins:string[]) => 
    {
        setSelectedCryptoCoins(coins);
    }

    return(
        <Container className="dataContainer" id="dataContainer">
            <Row className="chartFilterRow">
                <Col md="10">
                    <EHChartContainer cryptoData={cryptoData} 
                        energyData={energyData}/>
                </Col>
                <Col md="2">
                    <EHChartFilterContainer currentUser={currentUser} 
                        setCryptoCoinsForFilter={setCryptoCoinsForFilter}
                        setSelectedValueTypes={setSelectedValueTypes}
                        selectedValueTypes={selectedValueTypes}
                        setSlectedCountries={setSelectedCountries}
                        selectedCountries={selectedCountries}
                        setSelectedEnergyStateValueTypes={setSelectedEnergyStateValueTypes}
                        selectedEnergyStateValueTypes={selectedEnergyStateValueTypes}/>  
                </Col>
            </Row>
        </Container>
    )

}

export default EHDataContainer; 