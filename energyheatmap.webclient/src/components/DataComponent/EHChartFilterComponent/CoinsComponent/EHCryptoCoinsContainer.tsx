import React, { useEffect, useState } from "react";
import {
    useLoading,
    ThreeDots
} from '@agney/react-loading';
import { Container } from "react-bootstrap";
import Select from 'react-select'

//services
import { getCryptoCoins } from "../../../../services/httpService";

//models
import { User } from "../../../../models/User"


//styles
import "./EHCryptoCoinsContainer.css"

interface Props {
    currentUser: User;
    setCryptoCoinsForFilter: (coins: string[]) => void;
}

const EHCryptoCoinsContainer: React.FC<Props> = ({ currentUser, setCryptoCoinsForFilter }) => {
    const [isBusy, setIsBusy] = useState<boolean>(false);
    const [cryptoCoins, setCryptoCoins] = useState<{value:string, label:string}[]>([]);
    const { containerProps, indicatorEl } = useLoading({
        loading: true,
        indicator: <ThreeDots/>,
        loaderProps: {
        }
    });

    useEffect(() => {
        let fetchCryptoCoins = async () => {
            setIsBusy(true);
            let data = await getCryptoCoins(currentUser);

            if (data === null)
                return;

            const options = data.map(i => 
            {
                return { value: i as string, label: i as string};
            }); 

            setCryptoCoins(options);
            setIsBusy(false);
        }

        if(cryptoCoins.length === 0)
            setTimeout(() => fetchCryptoCoins(), 100);

    }, [isBusy, currentUser, cryptoCoins]);

    return (
        <Container className="cryptoCoinsContainer">
            {
                isBusy ? (
                    <section className="busyIndicator" {...containerProps}>
                        {indicatorEl}
                    </section>
                ) :  <Select options={cryptoCoins}                     
                            isSearchable={false}
                            isClearable={false}
                            isMulti={false}
                            onChange={(e) => 
                            {
                                setCryptoCoinsForFilter([e?.value as string])
                            }}/>
            }
        </Container>
    )
}

export default EHCryptoCoinsContainer;