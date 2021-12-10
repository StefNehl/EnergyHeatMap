import React from "react"
import { Route, Routes } from "react-router-dom";


import EHMapContainer from "../components/EHMapContainer"
import NavigationRibbonContainer from "../components/NavigationRibbonContainer"
import EHChartContainer from "../components/ChartComponent/EHChartContainer"

//models
import {User} from "../models/User"

interface Props{
    currentUser: User;
}

const MainPage: React.FC<Props> = ({ currentUser }) => 
{
    return (
        <div className="main-div">
            <NavigationRibbonContainer />
            <Routes>
                <Route path="/"  element={ <EHMapContainer currentUser={ currentUser }/> }/>
                <Route path="/charts" element={<EHChartContainer currentUser={ currentUser}/>}/>
                <Route path="/users" element={<div>Users</div>}/>
            </Routes>
        </div>
    );
}

export default MainPage;