import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container, Row } from "react-bootstrap";

//Components
import EHChartFilterContainer from "./EHChartFilterComponent/EHChartFilterContainer"
import EHChartContainer from "./EHChartContainer"

//services 
import { getCryptoCoinStatesFilteredAsync, getCryptoCoinStatesFilteredWithTypeAsync } from "../../services/httpService";

//models
import { User } from "../../models/User"




interface Props{
    currentUser: User;
}

const EHDataContainer: React.FC<Props> = ( { currentUser }) => 
{
    const [selectedCryptoCoins, setSelectedCryptoCoins] = useState<string[]>([]);
    const [selectedValueTypes, setSelectedValueTypes] = useState<string[]>(["Value"]);
    const [data, setData] = useState<unknown[]>([]);

    const [isBusy, setIsBusy] = useState<boolean>(false);
    const { containerProps, indicatorEl } = useLoading({
        loading: true,
        indicator: <ThreeDots/>,
        loaderProps: {
        }
    });


    let fetchCryptoCoinStates = async () => {
        setIsBusy(true);
        let newData = await getCryptoCoinStatesFilteredWithTypeAsync(
            currentUser, 
            selectedCryptoCoins, 
            selectedValueTypes);
        
        if(newData === null)
            return;
        console.log(newData.length + " items loaded");
        
        setData(newData);
        setIsBusy(false);
    };

    useEffect(() => 
    {   
        setTimeout(() => fetchCryptoCoinStates(), 100);        
    }, [selectedCryptoCoins, selectedValueTypes])

    let setCryptoCoinsForFilter = (coins:string[]) => 
    {
        setSelectedCryptoCoins(coins);
    }

    return(
        <Container>
            <Row>
                <EHChartFilterContainer currentUser={currentUser} 
                    setCryptoCoinsForFilter={setCryptoCoinsForFilter}
                    setSelectedValueTypes={setSelectedValueTypes}
                    selectedValueTypes={selectedValueTypes}/>                
            </Row>
            {
                isBusy ? (
                    <section className="busyIndicator" {...containerProps}>
                        {indicatorEl}
                    </section>
                ) : <div className="busyIndicator"/>
            }
            <Row>
            </Row>
            <Row>
                <EHChartContainer data={data}/>
            </Row>
        </Container>
    )

}

export default EHDataContainer; 