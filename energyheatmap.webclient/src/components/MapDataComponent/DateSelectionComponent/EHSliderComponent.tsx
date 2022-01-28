import React, { useEffect, useState } from "react";
import Slider from 'rc-slider';

//Styles
import 'rc-slider/assets/index.css';

//models
import { CountryDataGroupByDate } from "../../../models/CountryDataGroupByDate";

interface Props
{
    countryData:CountryDataGroupByDate[];
    setSelectedCountryData: (types:CountryDataGroupByDate) => void;
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
        var dataLength = Object.keys(countryData).length;
        if(dataLength !== 0)
        {
            var maxValue = dataLength - 1;
            console.log(maxValue);
            setSliderMaxValue(maxValue);
        }
    }, [countryData])

    return(
        <div className="sliderDiv" id="sliderDiv">
            <Slider min={minValue} max={sliderMaxValue} onChange={selectData}/>
        </div>
    )
}

export default EHSliderComponent;