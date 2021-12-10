import React, { useEffect, useState } from "react";
import {
    useLoading,
    Circles,
} from '@agney/react-loading';

//services
import { getCryptoCoins } from "../../services/httpService";

//models
import { User } from "../../models/User"
import { sleep } from "@amcharts/amcharts5/.internal/core/util/Time";

interface Props {
    currentUser: User;
}

const EHCryptoCoinsContainer: React.FC<Props> = ({ currentUser }) => {
    const [isBusy, setIsBusy] = useState<boolean>(false);
    const [cryptoCoins, setCryptoCoins] = useState<string[]>([]);
    const { containerProps, indicatorEl } = useLoading({
        loading: true,
        indicator: <Circles />
    });

    let testData = ["Test1", "Test2"];

    useEffect(() => {
        let fetchCryptoCoins = async () => {
            setIsBusy(true);
            let data = await getCryptoCoins(currentUser);

            if (data === null)
                return;

            console.log(data);
            setCryptoCoins(data);
            setIsBusy(false);
        }

        let busyTest = async () => 
        {
            if(cryptoCoins.length !== 0)
                return;
            

            setIsBusy(true);
            await sleep(5000);
            setCryptoCoins(testData);
            setIsBusy(false);
        }

        setTimeout(() => busyTest(), 1000);

    }, [isBusy, currentUser]);

    return isBusy ? (
        <section {...containerProps}>
            {indicatorEl}
        </section>
    ) : (
        <div>
        </div>
    )
}

export default EHCryptoCoinsContainer;