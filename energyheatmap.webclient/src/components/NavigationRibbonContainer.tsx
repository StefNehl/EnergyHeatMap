import React from "react";
import Container from 'react-bootstrap/Container'
import Navbar from 'react-bootstrap/Navbar'
import { Nav } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'

//Styles
import "./NavigationRibbonContainer.css"
import "../EnergyHeatMap.css"

interface Props{

}

const NavigationRibbonContainer: React.FC<Props> = () => 
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
        </Navbar>
    )
}

export default NavigationRibbonContainer