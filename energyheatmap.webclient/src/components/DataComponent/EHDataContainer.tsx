import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container, Row } from "react-bootstrap";

//Components
import EHChartFilterContainer from "./EHChartFilterComponent/EHChartFilterContainer"
import EHChartContainer from "./EHChartContainer"

//services 
import { getCryptoCoinStatesFilteredWithTypeAsync } from "../../services/httpCryptoCoinStatesService";
import { getEnergyStatesFilteredWithTypeAsync } from "../../services/httpEnergyStatesService";

//models
import { User } from "../../models/User"
import {CryptoStateData} from './../../models/CryptoStateData';
import { EnergyStateData } from "../../models/EnergyStateData";



interface Props{
    currentUser: User;
}

const EHDataContainer: React.FC<Props> = ( { currentUser }) => 
{
    const [selectedCryptoCoins, setSelectedCryptoCoins] = useState<string[]>([]);
    const [selectedValueTypes, setSelectedValueTypes] = useState<string[]>([]);
    const [selectedCountries, setSelectedCountries] = useState<string[]>([]);
    const [selectedEnergyStateValueTypes, setSelectedEnergyStateValueTypes] = useState<string[]>([]);
    const [cryptoData, setCryptoData] = useState<CryptoStateData[]>([]);
    const [energyData, setEnergyData] = useState<EnergyStateData[]>([]);

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
        console.log(newData.length + " crypto items loaded");
        
        setCryptoData(newData);
        setIsBusy(false);
    };

    let fetchEnergyStates = async () => {
        setIsBusy(true);
        let newData = await getEnergyStatesFilteredWithTypeAsync(
            currentUser, 
            selectedCountries, 
            selectedEnergyStateValueTypes);
        
        if(newData === null)
            return;
        console.log(newData.length + " energy items loaded");
        
        setEnergyData(newData);
        setIsBusy(false);
    };

    useEffect(() => 
    {   
        setTimeout(() => fetchCryptoCoinStates(), 100);        
        setTimeout(() => fetchEnergyStates(), 100);    
    }, [selectedCryptoCoins, selectedValueTypes, 
        selectedEnergyStateValueTypes, selectedEnergyStateValueTypes])

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
                    selectedValueTypes={selectedValueTypes}
                    setSlectedCountries={setSelectedCountries}
                    selectedCountries={selectedCountries}
                    setSelectedEnergyStateValueTypes={setSelectedEnergyStateValueTypes}
                    selectedEnergyStateValueTypes={selectedEnergyStateValueTypes}/>                
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
                <EHChartContainer cryptoData={cryptoData} 
                    energyData={energyData}/>
            </Row>
        </Container>
    )

}

export default EHDataContainer; 