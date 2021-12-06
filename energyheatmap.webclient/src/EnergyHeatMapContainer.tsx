import React, { useEffect, useState } from "react"
import { User } from "./models/User"
import { Roles } from "./models/Roles"

//Pages
import LogInPage from "./pages/LogInPage"

//Services
import {
    logInAsync
} from "../src/services/httpService"

interface Props {}

const initUser = 
{
    userId: -1,
    active:false,
    role: Roles.Foo,
    token: "",
    username: "Foo"
}

const EnergyHeatMapContainer: React.FC<Props> = () =>
{
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [currentUser, setCurrentUser] = useState<User>(initUser);
    const [isBusy, setIsBusy] = useState<boolean>(false);

    //effect
    useEffect(() => {

    });

    //methods
    const logIn = async(
        userName:string, 
        password:string
    ) : Promise<boolean> => 
    {
        setIsBusy(true);
        let user = await logInAsync(userName, password);
        setIsBusy(false);

        if(user == null)
        {
            alert("Wrong Pw or User");
            return false;
        }

        setCurrentUser(user);
        setIsLoggedIn(true);
        return true;
    }

    const logOut = () => {
        setCurrentUser(initUser);
        setIsLoggedIn(false);
      };

      return currentUser === initUser ? (
        <div className="auth-inner">
            <LogInPage logIn={logIn} isBusy={isBusy} />
      </div>
      ) : <div>"Worked"</div>
};

export default EnergyHeatMapContainer
