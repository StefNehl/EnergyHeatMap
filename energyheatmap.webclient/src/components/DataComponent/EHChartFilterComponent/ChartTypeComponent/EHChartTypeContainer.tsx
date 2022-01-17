import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container } from "react-bootstrap";

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
    const [cryptoValueTypes, setCryptoValueTypes] = useState<unknown[]>([]);
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

            setCryptoValueTypes(data);
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
                    <div>Test</div>
                )
            }
        </Container>
    )
}

export default EHChartTypeContainer;