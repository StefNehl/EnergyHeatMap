import React, {useState} from "react"
import { Container } from "react-bootstrap"
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import classNames from "classnames"
import EHMapContainer from "../components/EHMapContainer"

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
        <Container>
            <Routes>
                <Route path="/" element={ <EHMapContainer currentUser={ currentUser }/> }/>
                <Route path="/charts" element={<div>Charts</div>}/>
                <Route path="/users" element={<div>Users</div>}/>
            </Routes>
        </Container>
    );
}

export default MainPage;