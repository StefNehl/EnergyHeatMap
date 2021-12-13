import React, { useEffect, useState } from "react";
import {Container, Col, Row} from "react-bootstrap"
import EHChartCoinsContainer from "./CoinsComponent/EHCryptoCoinsContainer"
import EHCountryContainer from "./CountryComponent/EHCountryContainer"

//models
import { User } from "../../../models/User"

interface Props{
    currentUser: User;
}

const EHChartFilterContainer: React.FC<Props> = ({ currentUser }) =>
{
    return(
        <Container>
            <Row>
                <Col>
                    <EHCountryContainer currentUser={currentUser}/>
                </Col>
                <Col>
                    <EHChartCoinsContainer currentUser={currentUser}/>
                </Col>
            </Row>
        </Container>
    );
} 

export default EHChartFilterContainer;