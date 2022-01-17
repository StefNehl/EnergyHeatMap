import React from "react";
import { Container } from "react-bootstrap";

//models
import { User } from "../../../../models/User"

interface Props{
    currentUser: User;
}

const EHCountryContainer: React.FC<Props> = ({ currentUser }) =>
{
    return(
        <Container>
            <div>Country Dropdown</div>
        </Container>
    );
} 

export default EHCountryContainer;