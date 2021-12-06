import { User } from "../models/User"

const url = "https://localhost:7176"
const AUTHENTICATE = "/users/authenticate";

const CRYPTOCOINSTATES_GET_ALL = "/cryptocoinstates";

export async function logInAsync(
    username:string,
    password:string
): Promise<User | null>
{
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

    try 
    {
        let response = await fetch(url + AUTHENTICATE, requestOptions);
    
        if (response.status === 400) 
          return null;
    
        return (await response.json()) as User;
    } 
    catch (error) 
    {
        console.log(error);
        throw new Error("API call " + AUTHENTICATE + " throws an exception");
    }
}