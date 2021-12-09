import React, { useEffect, useState } from "react";
import { MapContainer , TileLayer } from 'react-leaflet'
import L from "leaflet";
import 'leaflet/dist/leaflet.css';

//models
import { User } from "../models/User"

interface Props{
    currentUser: User;
}

const EHMapContainer: React.FC<Props> = 
({
    currentUser
}) => 
{
    const [map, setMap] = useState<any>(null);
    currentUser = currentUser;
    useEffect(() => 
    {
        if(map !== null)
        {
            let mapObject = map as L.Map;
            if(mapObject !== null)
                mapObject.invalidateSize();
        }
    });
    
    //return(<div className="leaflet-container">Test</div>)
    return (
        <MapContainer
            id="leaflet-map"
            className="leaflet-container"
            center={[47.617324, 13.297555]}
            zoom={5}
            scrollWheelZoom={true}
            whenCreated={setMap}>
                <TileLayer 
                    attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />

                <TileLayer
                    attribution='Icons made by <a href="https://www.flaticon.com/authors/freepik">Freepik</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a> '
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
        </MapContainer>
    )
};


export default EHMapContainer