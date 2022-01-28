import {CountryData} from "./CountryData"

export interface CountryDataGroupByCountry
{
    country: string;
    values: CountryData[];
}