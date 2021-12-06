import React, {useState} from "react"
import { Container } from "react-bootstrap"
import { Route, Routes } from "react-router-dom";
import classNames from "classnames"

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
                <Route path="/">
                    <div className="App">
                        
                    </div>
                </Route>
                <Route path="/charts">
                    <div>Charts</div>
                </Route>
                <Route path="/users">
                    <div>Users</div>
                </Route>
            </Routes>
        </Container>
    );
}

export default MainPage;