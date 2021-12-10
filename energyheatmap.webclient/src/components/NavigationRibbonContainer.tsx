import React from "react";
import Container from 'react-bootstrap/Container'
import Navbar from 'react-bootstrap/Navbar'
import { Button, Nav } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'

//Styles
import "./NavigationRibbonContainer.css"
import "../EnergyHeatMap.css"

interface Props{
    logOut(): void;
}

const NavigationRibbonContainer: React.FC<Props> = ({ logOut }) => 
{
    return(
        <Navbar className="navContainer" bg="dark" variant="dark">
            <Container className="navContent">
                <Navbar.Brand>Energy Heat Map</Navbar.Brand>
                <Nav className="me-auto" >
                    <Nav.Link href="/">Map</Nav.Link>
                    <Nav.Link href="/charts">Charts</Nav.Link>
                    <Nav.Link href="/users">Users</Nav.Link>
                </Nav>
            </Container>
            <Button variant="dark" onClick={logOut}>Log Out</Button>
        </Navbar>
    )
}

export default NavigationRibbonContainer