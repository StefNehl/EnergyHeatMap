import React, { useEffect, useState } from "react";
import Slider from 'rc-slider';

//Styles
import 'rc-slider/assets/index.css';

//models
import { CountryData } from "../../../models/CountryData";

interface Props
{
    countryData:CountryData[];
    setSelectedCountryData: (types:CountryData) => void;
}

const EHSliderComponent : React.FC<Props> = ({countryData, setSelectedCountryData}) => 
{
    const [sliderMaxValue, setSliderMaxValue] = useState<number>(0);
    let minValue = 0;

    let selectData = async (e : number) => 
    {
        var selectedData = countryData[e];
        setSelectedCountryData(selectedData);
    }

    useEffect(() => 
    {
        if(countryData.length !== 0)
            setSliderMaxValue(countryData.length-1);
    }, [countryData])

    return(
        <div className="sliderDiv" id="sliderDiv">
            <Slider min={minValue} max={sliderMaxValue} onChange={selectData}/>
        </div>
    )
}

export default EHSliderComponent;