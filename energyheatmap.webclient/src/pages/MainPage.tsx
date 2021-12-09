import React, {useState} from "react"
import { Container } from "react-bootstrap"
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";


import EHMapContainer from "../components/EHMapContainer"
import NavigationRibbonContainer from "../components/NavigationRibbonContainer"

//models
import {User} from "../models/User"

interface Props{
    currentUser: User;
}

const MainPage: React.FC<Props> = 
({
    currentUser
}) => 
{
    return (
        <div className="main-div">
            <NavigationRibbonContainer />
            <Routes>
                <Route path="/home" element={ <EHMapContainer currentUser={ currentUser }/> }/>
                <Route path="/charts" element={<div>Charts</div>}/>
                <Route path="/users" element={<div>Users</div>}/>
            </Routes>
        </div>
    );
}

export default MainPage;