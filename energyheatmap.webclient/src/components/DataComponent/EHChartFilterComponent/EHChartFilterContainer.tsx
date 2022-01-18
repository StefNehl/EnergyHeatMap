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
}

const EHChartFilterContainer: React.FC<Props> = ({ 
    currentUser, 
    setCryptoCoinsForFilter, 
    setSelectedValueTypes,
    selectedValueTypes }) =>
{
    return(
        <Container>
            <Row>
                <Col>
                    <EHChartTypeContainer currentUser={currentUser} 
                        setSelectedValueTypes={setSelectedValueTypes}
                        selectedValueTypes={selectedValueTypes}/>
                </Col>
                <Col>
                    <EHCountryContainer currentUser={currentUser}/>
                </Col>
                <Col>
                    <EHChartCoinsContainer currentUser={currentUser} 
                        setCryptoCoinsForFilter={setCryptoCoinsForFilter}/>
                </Col>
            </Row>
        </Container>
    );
} 

export default EHChartFilterContainer;