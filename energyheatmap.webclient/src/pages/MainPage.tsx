import React from "react"
import { Route, Routes } from "react-router-dom";


import EHMapContainer from "../components/EHMapContainer"
import EHAMMapContainer from "../components/MapComponent/EHAMMapContainer"
import NavigationRibbonContainer from "../components/NavigationRibbonContainer"
import EHDataContainer from "../components/DataComponent/EHDataContainer"

//models
import {User} from "../models/User"

interface Props{
    currentUser: User;
    logOut(): void;
}

const MainPage: React.FC<Props> = ({ currentUser, logOut }) => 
{
    return (
        <div className="main-div">
            <NavigationRibbonContainer  logOut={logOut}/>
            <Routes>
                <Route path="/"  element={ <EHAMMapContainer currentUser={ currentUser }/> }/>
                <Route path="/charts" element={<EHDataContainer currentUser={ currentUser}/>}/>
                <Route path="/users" element={<div>Users</div>}/>
            </Routes>
        </div>
    );
}

export default MainPage;