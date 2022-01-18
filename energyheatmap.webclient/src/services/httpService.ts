import { User } from "../models/User"
import { CryptoStateData } from "../models/CryptoStateData";

const url = "https://localhost:7176"
const AUTHENTICATE = "/users/authenticate";

const CRYPTOCOINSTATES_GET_ALL = "/cryptocoinstates";
const CRYPTOCOINSTATES_GET_FILTERED = "/cryoticoinstatesfiltered";
const CRYPTOCOINSTATES_GET_FILTERED_WITHTYPE = "/cryoticoinstatesfilteredwithtype";
const CRYPTOCOINSTATES_GET_FILTERED_TYPES = "/cryptovaluetypes";
const CRYPTOCOINSTATES_GET_COIN_FILTER = "/?coinnames=";
const CRYPTOCOINSTATES_GET_STARTDATE = "&startdate=2015.01.01";
const CRYPTOCOINSTATES_GET_ENDDATE = "&enddate=2022.01.01";
const CRYPTOCOINS_GET_ALL = "/cryptocoins";

export async function logInAsync(
    username: string,
    password: string
): Promise<User | null> {
    const userData = { username, password };
    const requestOptions: RequestInit =
    {
        method: "POST",
        body: JSON.stringify(userData),
        headers:
        {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
        },
    };

    try {
        let response = await fetch(url + AUTHENTICATE, requestOptions);

        if (response.status === 400)
            return null;

        return (await response.json()) as User;
    }
    catch (error) {
        console.log(error);
        throw new Error("API call " + AUTHENTICATE + " throws an exception");
    }
}

export async function getCryptoCoinStatesAsync(currentUser: User): Promise<unknown[] | null> 
{
    try 
    { 
        let result = await callService<unknown[]>(
            CRYPTOCOINSTATES_GET_ALL,
            getAuthRequestInit(currentUser.token, "GET"));
        
        return result;
    }
    catch (error) {
        console.log(error);
        throw new Error("API call " + CRYPTOCOINSTATES_GET_ALL + " throws an exception");
    }
}

export async function getCryptoCoinStatesFilteredAsync(
    currentUser: User, 
    coins:string[]) : Promise<unknown[] | null>
{
    try
    {
        let coinsString = coins.join();
        var filterString = CRYPTOCOINSTATES_GET_FILTERED + 
            CRYPTOCOINSTATES_GET_COIN_FILTER + 
            coinsString + 
            CRYPTOCOINSTATES_GET_STARTDATE + 
            CRYPTOCOINSTATES_GET_ENDDATE; 

        let result = await callService<unknown[]>(
            filterString,
            getAuthRequestInit(currentUser.token, "GET"));

        return result;
    }
    catch (error) 
    {
        console.log(error);
        throw new Error("API call " + CRYPTOCOINSTATES_GET_FILTERED + " throws an exception");
    }
}

export async function getCryptoCoinStatesFilteredWithTypeAsync(
    currentUser: User, 
    coins:string[], 
    types:string[]) : Promise<CryptoStateData[] | null>
{
    try
    {
        let coinsString = coins.join();
        let typesString = types.join();
        var filterString = CRYPTOCOINSTATES_GET_FILTERED_WITHTYPE + 
            CRYPTOCOINSTATES_GET_COIN_FILTER + 
            coinsString + 
            "&types=" + typesString +
            CRYPTOCOINSTATES_GET_STARTDATE + 
            CRYPTOCOINSTATES_GET_ENDDATE; 

        let result = await callService<CryptoStateData[]>(
            filterString,
            getAuthRequestInit(currentUser.token, "GET"));

        return result;
    }
    catch (error) 
    {
        console.log(error);
        throw new Error("API call " + CRYPTOCOINSTATES_GET_FILTERED_WITHTYPE + " throws an exception");
    }
}

export async function getCryptoCoinStatesValueTypes(
    currentUser: User) : Promise<unknown[] | null>
{
    try
    {
        let result = await callService<unknown[]>(
            CRYPTOCOINSTATES_GET_FILTERED_TYPES,
            getAuthRequestInit(currentUser.token, "GET"));

        //filter all, because of multi select
        let filteredResult = result?.filter(i => i !== "All");
        return filteredResult as string[];
    }
    catch (error) 
    {
        console.log(error);
        throw new Error("API call " + CRYPTOCOINSTATES_GET_FILTERED + " throws an exception");
    }
}

export async function getCryptoCoins(currentUser: User): Promise<string[] | null>
{
    try
    {
        let result = await callService<string[]>(
            CRYPTOCOINS_GET_ALL, 
            getAuthRequestInit(currentUser.token, "GET"));
            
        return result;
    }
    catch(error)
    {
        console.log(error);
        throw new Error("API call " + AUTHENTICATE + " throws an exception");
    }
}

function getAuthRequestInit(token: string, method: string): RequestInit {
    return {
        method: method,
        headers: authHeader(token)
    }

}

function authHeader(token: string): {} {
    return { Authorization: "Bearer " + token };
}

async function callService<T>(
    call: string,
    requestInit?: RequestInit
): Promise<T | null> {
    try {
        let response = await fetch(url + call, requestInit);
        return (await response.json()) as T;
    } catch (error) {
        console.log(error);
        return null;
    }
}