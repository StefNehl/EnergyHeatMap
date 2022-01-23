import React from "react";
import {Container, Col, Row} from "react-bootstrap"
import EHChartCoinsContainer from "./CoinsComponent/EHCryptoCoinsContainer"
import EHCountryContainer from "./CountryComponent/EHCountryContainer"
import EHChartTypeContainer from "./ChartTypeComponent/EHChartTypeContainer"
import EHEnergyStateValueContainer from "./EnergyStateValueTypeComponent/EHEnergyStateValueTypeContainer"

//models
import { User } from "../../../models/User"

//Style
import './EHChartFilterContainer.css'

interface Props{
    currentUser: User;
    setCryptoCoinsForFilter: (coins: string[]) => void;
    setSelectedValueTypes: (types:string[]) => void;
    selectedValueTypes: string[];
    setSlectedCountries: (types:string[]) => void;
    selectedCountries: string[];
    setSelectedEnergyStateValueTypes: (types:string[]) => void;
    selectedEnergyStateValueTypes: string[];
}

const EHChartFilterContainer: React.FC<Props> = ({ 
    currentUser, 
    setCryptoCoinsForFilter, 
    setSelectedValueTypes,
    selectedValueTypes, 
    setSlectedCountries, 
    selectedCountries,
    setSelectedEnergyStateValueTypes, 
    selectedEnergyStateValueTypes  }) =>
{
    return(
        <Container id="filterContainer" className="filterContainer">
            <h5>Filter</h5>
            <Row>
                <Col>
                    <EHChartCoinsContainer currentUser={currentUser} 
                        setCryptoCoinsForFilter={setCryptoCoinsForFilter}/>
                    <EHChartTypeContainer currentUser={currentUser} 
                        setSelectedValueTypes={setSelectedValueTypes}
                        selectedValueTypes={selectedValueTypes}/>
                </Col>
            </Row>
            <Row>
                <Col>
                    <EHCountryContainer currentUser={currentUser}
                        setSlectedCountries={setSlectedCountries}
                        selectedCountries={selectedCountries}/>
                    <EHEnergyStateValueContainer currentUser={currentUser}
                        setSelectedEnergyStateValueTypes={setSelectedEnergyStateValueTypes}
                        selectedEnergyStateValueTypes={selectedEnergyStateValueTypes}/>
                </Col>
            </Row>

        </Container>
    );
} 

export default EHChartFilterContainer;