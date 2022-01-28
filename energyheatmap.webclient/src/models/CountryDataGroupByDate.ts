import {CountryData} from "./CountryData"

export interface CountryDataGroupByDate
{
    dateTime: Date;
    values: CountryData[];
}