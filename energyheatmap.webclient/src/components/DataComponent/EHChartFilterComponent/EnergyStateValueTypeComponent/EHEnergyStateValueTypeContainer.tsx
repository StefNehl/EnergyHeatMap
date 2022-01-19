import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container } from "react-bootstrap";
import Select from 'react-select'

//services
import { getEnergyStateValueTypes } from "../../../../services/httpEnergyStatesService";

//models
import { User } from "../../../../models/User"

//styles
import "./EHEnergyStateValueTypeContainer.css"

interface Props{
    currentUser: User;
    setSelectedEnergyStateValueTypes: (types:string[]) => void;
    selectedEnergyStateValueTypes: string[];
}

const EHEnergyStateValueContainer: React.FC<Props> = ({ currentUser, 
    setSelectedEnergyStateValueTypes, 
    selectedEnergyStateValueTypes }) =>
{
    const [energyStateValueTypes, setEnergyStateValueTypes] = useState<{value:string, label:string}[]>([]);
    const [isBusy, setIsBusy] = useState<boolean>(false);
    const { containerProps, indicatorEl } = useLoading({
        loading: true,
        indicator: <ThreeDots/>,
        loaderProps: {
        }
    });

    useEffect(() =>
    {
        let fetchCryptoTypes = async () => {
            setIsBusy(true);
            let data = await getEnergyStateValueTypes(currentUser);

            if (data === null)
                return;

            const options = data.map(i => 
            {
                return { value: i.type, label: i.name};
            }); 

            setEnergyStateValueTypes(options);
            setIsBusy(false);
        }

        if(energyStateValueTypes.length === 0)
            setTimeout(() => fetchCryptoTypes(), 100);
    })

    return(
        <Container className="energyStateContainer">
            {
                isBusy ? (
                    <section className="busyIndicator" {...containerProps}>
                        {indicatorEl}
                    </section>
                ) : (
                    <Select options={energyStateValueTypes}                     
                        isSearchable={false}
                        isClearable={false}
                        isMulti={true}
                        onChange={(e) => 
                        {
                            if(e !== undefined)
                            {
                                var values = e.map(i => i.value);
                                setSelectedEnergyStateValueTypes(values);
                            }
                        }}/>
                )
            }
        </Container>
    )
} 

export default EHEnergyStateValueContainer;