import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container } from "react-bootstrap";
import Select from 'react-select'

//services
import { getCryptoCoinStatesValueTypes } from "../../../../services/httpService";

//models
import { User } from "../../../../models/User"

interface Props{
    currentUser: User;
    setSelectedValueType: (type:string) => void;
}

const EHChartTypeContainer : React.FC<Props> = ({currentUser, setSelectedValueType}) => 
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
                        isMulti={false}
                        onChange={(e) => 
                        {
                            if(e?.value !== undefined)
                                setSelectedValueType(e?.value as string);
                        }}/>
                )
            }
        </Container>
    )
}

export default EHChartTypeContainer;