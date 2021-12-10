import React, { useState, useEffect } from "react"
import { User } from "./models/User"
import { Roles } from "./models/Roles"

//Pages
import LogInPage from "./pages/LogInPage"
import MainPage from "./pages/MainPage"

//Services
import {
    logInAsync
} from "../src/services/httpService"

// stylesheet
import "./EnergyHeatMap.css"

interface Props { }

const initUser =
{
    userId: -1,
    active: false,
    role: Roles.Foo,
    token: "",
    username: "Foo"
}

// const devUser =
// {
//     userId: 0,
//     active: true,
//     role: Roles.Admin,
//     token: "",
//     username: "Admin"
// }

const EnergyHeatMapContainer: React.FC<Props> = () => {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [currentUser, setCurrentUser] = useState<User>(initUser);
    const [isBusy, setIsBusy] = useState<boolean>(false);

    //useEffect
    useEffect(() => {
        let storedUserString = localStorage.getItem("user");
        if(storedUserString !== null)
        {
            let storedUser = JSON.parse(storedUserString) as User;
            setLogInState(storedUser);
        }
        else
            logOut();

    }, [])

    //methods
    const logIn = async (
        userName: string,
        password: string
    ): Promise<boolean> => {
        setIsBusy(true);
        let user = await logInAsync(userName, password);
        setIsBusy(false);

        if (user == null) {
            alert("Wrong Pw or User");
            return false;
        }
        
        setLogInState(user);
        return true;
    }

    const setLogInState = (user: User) =>
    {
        localStorage.setItem("user", JSON.stringify(user));
        setCurrentUser(user);
        setIsLoggedIn(true);
    }

    const logOut = () => {
        localStorage.removeItem("user");
        setCurrentUser(initUser);
        setIsLoggedIn(false);
    };

    return isLoggedIn ?(
            <div>
                <MainPage currentUser={currentUser} logOut={logOut} />
            </div>
        ) :(
            <div className="auth-inner">
                <LogInPage logIn={logIn} isBusy={isBusy} />
            </div>
        )
};

export default EnergyHeatMapContainer
