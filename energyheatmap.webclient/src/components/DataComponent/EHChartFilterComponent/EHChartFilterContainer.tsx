import React from "react";
import {Container, Col, Row} from "react-bootstrap"
import EHChartCoinsContainer from "./CoinsComponent/EHCryptoCoinsContainer"
import EHCountryContainer from "./CountryComponent/EHCountryContainer"

//models
import { User } from "../../../models/User"

interface Props{
    currentUser: User;
    setCryptoCoinsForFilter: (coins: string[]) => void;
}

const EHChartFilterContainer: React.FC<Props> = ({ currentUser, setCryptoCoinsForFilter }) =>
{
    return(
        <Container>
            <Row>
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