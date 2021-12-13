import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import { Container, Row, Col } from "react-bootstrap";

import EHCryptoCoinsContainer from "./EHChartFilterComponent/CoinsComponent/EHCryptoCoinsContainer"

//services 
import { createXYChart } from "../../services/chartService";
import { getCryptoCoinStatesAsync } from "../../services/httpService";

//models
import { User } from "../../models/User"

//import css
import './EHChartContainer.css'




interface Props{
    currentUser: User;
}

const EHChartContainer: React.FC<Props> = ( { currentUser }) => 
{
    useEffect(() => 
    {      
        let root = am5.Root.new("chartdiv");

        let series = createXYChart(root);



        let fetchCryptoCoinStates = async () => {
            let data = await getCryptoCoinStatesAsync(currentUser);
            
            if(data === null)
                return
            console.log(data);
            series.data.setAll(data as unknown[]);
        };


        setTimeout(() => fetchCryptoCoinStates(), 1000);

        
    }, [currentUser])

    return(
        <Container>
            <Row>
                <Col>
                    DropDown
                </Col>
                <Col>
                    <EHCryptoCoinsContainer currentUser={currentUser}/>
                </Col>
            </Row>
            <Row>
                <div id="chartdiv" className="chartdiv" />
            </Row>
        </Container>
    )

}

export default EHChartContainer; 