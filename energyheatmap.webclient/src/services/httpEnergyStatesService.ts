import { User } from "../models/User"

import { getAuthRequestInit } from "./httpService"
import { callService } from "./httpService"

const COUNTRIES_GET_ALL = "/countries";
const ENERGYSTATE_VALUE_TYPES = "/energystatevaluetypes";

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

export async function getEnergyStateValueTypes(currentUser: User): 
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