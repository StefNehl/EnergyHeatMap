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
    setSelectedValueType: (type:string) => void;
}

const EHChartFilterContainer: React.FC<Props> = ({ currentUser, setCryptoCoinsForFilter, setSelectedValueType }) =>
{
    return(
        <Container>
            <Row>
                <Col>
                    <EHChartTypeContainer currentUser={currentUser} 
                        setSelectedValueType={setSelectedValueType}/>
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