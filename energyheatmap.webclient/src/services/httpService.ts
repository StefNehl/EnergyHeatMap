import { User } from "../models/User"

const url = "https://localhost:7176"
const AUTHENTICATE = "/users/authenticate";

const CRYPTOCOINSTATES_GET_ALL = "/cryptocoinstates";

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

export async function getCryptoCoinStatesAsync(currentUser: User): Promise<unknown[] | null> {
    try { 
        let result = await callService<unknown[]>(
            CRYPTOCOINSTATES_GET_ALL,
            getAuthRequestInit(currentUser.token, "GET"));
        
        return result;
    }
    catch (error) {
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