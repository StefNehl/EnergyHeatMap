import React from "react";
import {Container, Col, Row} from "react-bootstrap"
import EHChartCoinsContainer from "./CoinsComponent/EHCryptoCoinsContainer"
import EHCountryContainer from "./CountryComponent/EHCountryContainer"
import EHChartTypeContainer from "./ChartTypeComponent/EHChartTypeContainer"

//models
import { User } from "../../../models/User"

interface Props{
    currentUser: User;
    setCryptoCoinsForFilter: (coins: string[]) => void;
    setSelectedValueTypes: (types:string[]) => void;
    selectedValueTypes: string[];
    setSlectedCountries: (types:string[]) => void;
    selectedCountries: string[];
}

const EHChartFilterContainer: React.FC<Props> = ({ 
    currentUser, 
    setCryptoCoinsForFilter, 
    setSelectedValueTypes,
    selectedValueTypes, 
    setSlectedCountries, 
    selectedCountries }) =>
{
    return(
        <Container>
            <Row>
                <Col>
                    Crypto Coin Filter
                    <EHChartCoinsContainer currentUser={currentUser} 
                        setCryptoCoinsForFilter={setCryptoCoinsForFilter}/>
                    <EHChartTypeContainer currentUser={currentUser} 
                        setSelectedValueTypes={setSelectedValueTypes}
                        selectedValueTypes={selectedValueTypes}/>
                </Col>
                <Col>

                </Col>
                <Col>
                    Energy Filter
                    <EHCountryContainer currentUser={currentUser}
                        setSlectedCountries={setSlectedCountries}
                        selectedCountries={selectedCountries}/>
                </Col>
            </Row>
        </Container>
    );
} 

export default EHChartFilterContainer;