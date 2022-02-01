import React, { useEffect, useState } from "react";
import ReactSlider from "react-slider";

//Styles

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
            setSliderMaxValue(maxValue);
        }
    }, [countryData])

    return(
        <div className="sliderDiv" id="sliderDiv">
            <ReactSlider 
                className="horizontal-slider" 
                thumbClassName="slider-thumb"
                trackClassName="slider-track"
                min={0}
                max={sliderMaxValue}
                renderThumb={(props, state) => <div {...props}>{state.valueNow}</div>}
                />
        </div>
    )
}

export default EHSliderComponent;