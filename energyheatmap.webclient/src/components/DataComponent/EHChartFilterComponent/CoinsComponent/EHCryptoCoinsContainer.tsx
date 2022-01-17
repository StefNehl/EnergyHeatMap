import React, { useEffect, useState } from "react";
import {
    useLoading,
    ThreeDots
} from '@agney/react-loading';
import  EHChartCryptoCoinsListContainer  from "./EHChartCryptoCoinsListContainer"
import { Container } from "react-bootstrap";

//services
import { getCryptoCoins } from "../../../../services/httpService";

//models
import { User } from "../../../../models/User"
import { sleep } from "@amcharts/amcharts5/.internal/core/util/Time";


//styles
import "./EHCryptoCoinsContainer.css"

interface Props {
    currentUser: User;
    setCryptoCoinsForFilter: (coins: string[]) => void;
}

const EHCryptoCoinsContainer: React.FC<Props> = ({ currentUser, setCryptoCoinsForFilter }) => {
    const [isBusy, setIsBusy] = useState<boolean>(false);
    const [cryptoCoins, setCryptoCoins] = useState<string[]>([]);
    const { containerProps, indicatorEl } = useLoading({
        loading: true,
        indicator: <ThreeDots/>,
        loaderProps: {
        }
    });

    useEffect(() => {
        let fetchCryptoCoins = async () => {
            setIsBusy(true);
            await sleep(100);
            let data = await getCryptoCoins(currentUser);

            if (data === null)
                return;

            setCryptoCoins(data);
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
                ) : (
                    <EHChartCryptoCoinsListContainer coins={cryptoCoins}
                        setCryptoCoinsForFilter={setCryptoCoinsForFilter} />
                )
            }
        </Container>
    )
}

export default EHCryptoCoinsContainer;