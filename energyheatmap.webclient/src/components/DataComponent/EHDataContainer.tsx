import React, { useEffect, useState } from "react";

import { Container, Row } from "react-bootstrap";

import EHChartFilterContainer from "./EHChartFilterComponent/EHChartFilterContainer"
import EHChartContainer from "./EHChartContainer"

//services 
import { getCryptoCoinStatesFilteredAsync } from "../../services/httpService";

//models
import { User } from "../../models/User"




interface Props{
    currentUser: User;
}

const EHDataContainer: React.FC<Props> = ( { currentUser }) => 
{
    const [selectedCryptoCoins, setSelectedCryptoCoins] = useState<string[]>([]);
    const [data, setData] = useState<unknown[]>([]);


    let fetchCryptoCoinStates = async () => {
        let newData = await getCryptoCoinStatesFilteredAsync(currentUser, selectedCryptoCoins);
        
        if(newData === null)
            return;
        console.log(newData.length + " items loaded");
        setData(newData);
    };

    useEffect(() => 
    {   
        setTimeout(() => fetchCryptoCoinStates(), 1000);
    }, [selectedCryptoCoins])

    let setCryptoCoinsForFilter = (coins:string[]) => 
    {
        setSelectedCryptoCoins(coins);
    }

    return(
        <Container>
            <Row>
                <EHChartFilterContainer currentUser={currentUser} 
                    setCryptoCoinsForFilter={setCryptoCoinsForFilter}/>                
            </Row>
            <Row>
                <EHChartContainer data={data}/>
            </Row>
        </Container>
    )

}

export default EHDataContainer; 