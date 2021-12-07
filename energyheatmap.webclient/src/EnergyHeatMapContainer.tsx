import React, { useEffect, useState } from "react"
import { User } from "./models/User"
import { Roles } from "./models/Roles"

//Pages
import LogInPage from "./pages/LogInPage"
import MainPage from "./pages/MainPage"

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

const devUser =           
{
    userId:0, 
    active: true,
    role: Roles.Admin,
    token: "",
    username: "Admin" 
}

const EnergyHeatMapContainer: React.FC<Props> = () =>
{
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [currentUser, setCurrentUser] = useState<User>(devUser);
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

      return currentUser === initUser ? 
      (
        <div className="auth-inner">
            <LogInPage logIn={logIn} isBusy={isBusy} />
      </div>
      ) : 
      (
          <div>
            <MainPage currentUser={currentUser}/>
          </div>
      )
};

export default EnergyHeatMapContainer
