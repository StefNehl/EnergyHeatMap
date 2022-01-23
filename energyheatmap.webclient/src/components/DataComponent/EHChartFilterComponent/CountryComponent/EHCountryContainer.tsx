import React, { useEffect, useState } from "react";
import { useLoading, ThreeDots } from '@agney/react-loading';
import { Container } from "react-bootstrap";
import Select from 'react-select'

//services
import { getCountries } from "../../../../services/httpEnergyStatesService";

//models
import { User } from "../../../../models/User"

//styles
import "./EHCountryContainer.css"

interface Props{
    currentUser: User;
    setSlectedCountries: (types:string[]) => void;
    selectedCountries: string[];
}

const EHCountryContainer: React.FC<Props> = ({ currentUser, setSlectedCountries, selectedCountries }) =>
{
    const [countries, setCountries] = useState<{value:string, label:string}[]>([]);
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
            let data = await getCountries(currentUser);

            if (data === null)
                return;

            const options = data.map(i => 
            {
                return { value: i as string, label: i as string};
            }); 

            setCountries(options);
            setIsBusy(false);
        }

        if(countries.length === 0)
            setTimeout(() => fetchCryptoTypes(), 100);
    })

    return(
        <Container className="countriesContainer">
            {
                isBusy ? (
                    <section className="busyIndicator" {...containerProps}>
                        {indicatorEl}
                    </section>
                ) : (
                    <Select options={countries}                     
                        isSearchable={true}
                        isClearable={false}
                        isMulti={true}
                        placeholder="Select countries..."
                        onChange={(e) => 
                        {
                            if(e !== undefined)
                            {
                                var values = e.map(i => i.value);
                                setSlectedCountries(values);
                            }
                        }}/>
                )
            }
        </Container>
    )
} 

export default EHCountryContainer;