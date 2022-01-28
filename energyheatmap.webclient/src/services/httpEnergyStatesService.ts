
//Models
import { User } from "../models/User"
import { EnergyStateData } from "../models/EnergyStateData"
import { CountryData } from "../models/CountryData"
import { CountryDataGroupByCountry } from "../models/CountryDataGroupByCountry"
import { CountryDataGroupByDate } from "../models/CountryDataGroupByDate"

//Services
import { getAuthRequestInit } from "./httpService"
import { callService } from "./httpService"


//Const
const COUNTRIES_GET_ALL = "/countries";
const COUNTRIES_DATA_GET_ALL = "/countriesdata";
const COUNTRIES_DATA_GROUPEDBY_COUNTRY_GET_ALL = "/countriesdatagroupedbycountry";
const COUNTRIES_DATA_GROUPEDBY_DATE_GET_ALL = "/countriesdatagroupedbydate";

const ENERGYSTATE_VALUE_TYPES = "/energystatevaluetypes";

const ENERGYSTATEDATA_GET_FILTERED_TYPES = "/energystatedata";
const ENERGYSTATEDATA_GET_COUNTRIES_FILTER = "/?countries=";
const ENERGYSTATEDATA_GET_TYPES_FILTER = "&types=";
const ENERGYSTATEDATA_GET_STARTDATE = "&startdate=2012.09.01";
const ENERGYSTATEDATA_GET_ENDDATE = "&enddate=2023.01.01";

export async function getCountries(currentUser: User): Promise<string[] | null>
{
    try
    {
        let result = await callService<string[]>(
            COUNTRIES_GET_ALL, 
            getAuthRequestInit(currentUser.token, "GET"));
            
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + COUNTRIES_GET_ALL + " throws an exception");
    }
}

export async function getCountriesData(currentUser : User) : Promise<CountryData[] | null>
{
    try
    {
        let result = await callService<CountryData[]>(
            COUNTRIES_DATA_GET_ALL, 
            getAuthRequestInit(currentUser.token, "GET"));
            
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + COUNTRIES_DATA_GET_ALL + " throws an exception");
    }
}

export async function getCountriesDataGroupedByCountry(currentUser : User) : Promise<CountryDataGroupByCountry[] | null>
{
    try
    {
        let result = await callService<CountryDataGroupByCountry[]>(
            COUNTRIES_DATA_GROUPEDBY_COUNTRY_GET_ALL, 
            getAuthRequestInit(currentUser.token, "GET"));
            
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + COUNTRIES_DATA_GROUPEDBY_COUNTRY_GET_ALL + " throws an exception");
    }
}

export async function getCountriesDataGroupedByDate(currentUser : User) : Promise<CountryDataGroupByDate[] | null>
{
    try
    {
        let result = await callService<CountryDataGroupByDate[]>(
            COUNTRIES_DATA_GROUPEDBY_DATE_GET_ALL, 
            getAuthRequestInit(currentUser.token, "GET"));
        
        console.log(result?.length)
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + COUNTRIES_DATA_GROUPEDBY_DATE_GET_ALL + " throws an exception");
    }
}

export async function getEnergyStateValueTypesAsync(currentUser: User): 
    Promise<{type:string, name:string}[] | null>
{
    try
    {
        let result = await callService<{type:string, name:string}[]>(
            ENERGYSTATE_VALUE_TYPES, 
            getAuthRequestInit(currentUser.token, "GET"));
            
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + ENERGYSTATE_VALUE_TYPES + " throws an exception");
    }
}

export async function getEnergyStatesFilteredWithTypeAsync(
    currentUser: User, 
    countries:string[], 
    types:string[]) : Promise<EnergyStateData[] | null>
{
    try
    {
        let countriesString = countries.join();
        let typesString = types.join();
        var filterString = ENERGYSTATEDATA_GET_FILTERED_TYPES + 
            ENERGYSTATEDATA_GET_COUNTRIES_FILTER + countriesString + 
            ENERGYSTATEDATA_GET_TYPES_FILTER + typesString +
            ENERGYSTATEDATA_GET_STARTDATE + 
            ENERGYSTATEDATA_GET_ENDDATE; 

        let result = await callService<EnergyStateData[]>(
            filterString,
            getAuthRequestInit(currentUser.token, "GET"));

        return result;
    }
    catch (error) 
    {
        console.log(error);
        throw new Error("API call " + ENERGYSTATEDATA_GET_FILTERED_TYPES + " throws an exception");
    }
}