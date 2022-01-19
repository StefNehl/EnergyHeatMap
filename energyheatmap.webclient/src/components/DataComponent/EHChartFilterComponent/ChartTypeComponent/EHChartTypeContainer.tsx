import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container } from "react-bootstrap";
import Select from 'react-select'

//services
import { getCryptoCoinStatesValueTypes } from "../../../../services/httpCryptoCoinStatesService";

//models
import { User } from "../../../../models/User"

//styles
import "./EHChartTypeContainer.css"

interface Props{
    currentUser: User;
    setSelectedValueTypes: (types:string[]) => void;
    selectedValueTypes: string[];
}

const EHChartTypeContainer : React.FC<Props> = ({currentUser, setSelectedValueTypes, selectedValueTypes}) => 
{
    const [cryptoValueTypes, setCryptoValueTypes] = useState<{value:string, label:string}[]>([]);
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
            let data = await getCryptoCoinStatesValueTypes(currentUser);

            if (data === null)
                return;

            const options = data.map(i => 
            {
                return { value: i as string, label: i as string};
            }); 

            setCryptoValueTypes(options);
            setIsBusy(false);
        }

        if(cryptoValueTypes.length === 0)
            setTimeout(() => fetchCryptoTypes(), 100);
    })

    return(
        <Container className="cryptoTypeContainer">
            {
                isBusy ? (
                    <section className="busyIndicator" {...containerProps}>
                        {indicatorEl}
                    </section>
                ) : (
                    <Select options={cryptoValueTypes}                     
                        isSearchable={false}
                        isClearable={false}
                        isMulti={true}
                        onChange={(e) => 
                        {
                            if(e !== undefined)
                            {
                                var values = e.map(i => i.value);
                                setSelectedValueTypes(values);
                            }
                            //setSelectedValueTypes([e?.value as string])
                        }}/>
                )
            }
        </Container>
    )
}

export default EHChartTypeContainer;