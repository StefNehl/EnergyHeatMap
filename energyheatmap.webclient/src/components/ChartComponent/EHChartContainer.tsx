import React, { useEffect, useState } from "react";
import * as am5 from "@amcharts/amcharts5";
import { Container, Row } from "react-bootstrap";

import EHChartFilterContainer from "./EHChartFilterComponent/EHChartFilterContainer"

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
    const [selectedCryptoCoins, setSelectedCryptoCoins] = useState<string[]>([]);

    useEffect(() => 
    {      
        let root = am5.Root.new("chartdiv");

        let series = createXYChart(root);



        let fetchCryptoCoinStates = async () => {
            let data = await getCryptoCoinStatesAsync(currentUser);
            
            if(data === null)
                return
            //console.log(data);
            series.data.setAll(data as unknown[]);
        };


        setTimeout(() => fetchCryptoCoinStates(), 1000);

        
    }, [currentUser])

    let setCryptoCoinsForFilter = (coins:string[]) => 
    {
        console.log(coins);
        setSelectedCryptoCoins(coins);
    }

    return(
        <Container>
            <Row>
                <EHChartFilterContainer currentUser={currentUser} 
                    setCryptoCoinsForFilter={setCryptoCoinsForFilter}/>                
            </Row>
            <Row>
                <div id="chartdiv" className="chartdiv" />
            </Row>
        </Container>
    )

}

export default EHChartContainer; 