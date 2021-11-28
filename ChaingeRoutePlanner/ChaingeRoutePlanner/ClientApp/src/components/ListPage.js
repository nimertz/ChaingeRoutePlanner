import React, { Component, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup,useMapEvents, Polyline as PL } from 'react-leaflet'
import {Button, Row, Col, Container, Input} from 'reactstrap';
import Polyline from '@mapbox/polyline'

function LocationMarker(props) {
    const [position, setPosition] = useState(null)
    const map = useMapEvents({
        click(e) {
            console.log('Click', e)
        },
        moveend() {
            let center = map.getCenter();
            let zoom = map.getZoom();
            console.log('Zoom:', zoom, 'Center:', center)
        },
        locationfound(e) {
            setPosition(e.latlng)
            map.flyTo(e.latlng, map.getZoom())
        },
    })

    return position === null ? null : (
        <Marker position={position}>
            <Popup>You are here</Popup>
        </Marker>
    )
}

export class ListPage extends Component {
    static displayName = ListPage.name;

    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            center: [55.66064229583371, 12.59125202894211],
            
            vehicles: [],
            shipments: [],
            selectedShipments: [],
            selectedVehicles: [],
            
            routes: [],
        };
    }

    componentDidMount() {
        this.populateLists();
    }
    
    handleCheckVehicle(id) {
        if (this.state.selectedVehicles.includes(id)) {
            this.setState({
                selectedVehicles: this.state.selectedVehicles.filter(v => v !== id)
            })
        } else {
            this.setState({
                selectedVehicles: [...this.state.selectedVehicles, id]
            })
        }
    }

    handleCheckShipment(id) {
        if (this.state.selectedShipments.includes(id)) {
            this.setState({
                selectedShipments: this.state.selectedShipments.filter(s => s !== id)
            })
        } else {
            this.setState({
                selectedShipments: [...this.state.selectedShipments, id]
            })
        }
    }
    
    async populateShipmentData() {
        const response = await fetch('Shipment/all');
        const data = await response.json();
        this.setState({ shipments: data});
    }

    async populateVehicleData() {
        const response = await fetch('Vehicle/all');
        const data = await response.json();
        this.setState({ vehicles: data});
    }

    async postRoutePlan() {
        const response = await fetch('RoutePlan', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                vehicleIds: this.state.selectedVehicles,
                shipmentIds: this.state.selectedShipments,
            })
        });
        const data = await response.json();
        //handle badrequest response
        if(data.code !== 0) {
            alert(data.error);
            return;
        } 
        
        if(data.routes.length === 0) {
            alert("No routes could be created using the provided shipments and vehicles\n" +
                " Please verify that the shipments and vehicles are valid");
            return;
        }
        
        
        console.log(data);
        //TODO make map adjust zoom & center based on route results
        this.setState({
            routes: data.routes,
            center: [data.routes[0].steps[0].location[1], data.routes[0].steps[0].location[0]]
        });
    }

    populateLists() {
        this.populateVehicleData();
        this.populateShipmentData()
        this.setState({ loading: false });
    }
    
    static decodeGeometry(geometry) {
        return Polyline.decode(geometry);
    }
    
    isButtonDisabled() {
        return this.state.selectedVehicles.length === 0 || this.state.selectedShipments.length === 0;
    }
    
    static convertDuration(duration) {
        //convert to hh:mm:ss
        let hours = Math.floor(duration / 3600);
        let minutes = Math.floor((duration - (hours * 3600)) / 60);
        let seconds = duration - (hours * 3600) - (minutes * 60);
        return `${hours}h ${minutes}m ${seconds}s`;
    }
    
    static generateRandomPolygonColor() {
        let colors = ["red" , "blue", "green", "purple","brown"];
        return colors[Math.floor(Math.random() * colors.length)];
    }

     renderVehicleList(vehicles) {
        return (
            <ul className="list-group">
                <h5>Vehicles</h5>
                {vehicles.map(vehicle =>
                    <li className="list-group-item" key={vehicle.id}>
                        <Input onChange={() =>this.handleCheckVehicle(vehicle.id) } className="form-check-input me-1" type="checkbox" value="Vehicle" aria-label="..."/>
                        {vehicle.id}: {vehicle.description} - {vehicle.capacity} kg
                    </li>
                )}
            </ul>
        );
    }
    
     renderShipmentList(shipments) {
        return (
            <ul className="list-group">
                <h5>Shipments</h5>
                {shipments.map(shipment =>
                    <li className="list-group-item" key={shipment.id}>
                        <Input onChange={() =>this.handleCheckShipment(shipment.id) } className="form-check-input me-1" type="checkbox" value="Shipment" aria-label="..."/>
                        {shipment.id}: {shipment.description}
                    </li>
                )}
            </ul>
        );
    }
    renderContents(vehicles, shipments) {
        let vehicleContent =  this.renderVehicleList(vehicles);
        let shipmentContent = this.renderShipmentList(shipments);

        return (
            <Row>
                <Col>
                    {vehicleContent}
                </Col>
                <Col>
                    {shipmentContent}
                    <div>
                        <Button onClick={() => this.postRoutePlan()} disabled={this.isButtonDisabled()}
                                color="primary">
                            Create route plan
                        </Button>
                    </div>
                </Col>
                <Col xs={6}>
                    <MapContainer center={this.state.center} zoom={15} scrollWheelZoom={true} eventHandlers={{
                        click: () => {
                            console.log('map clicked')
                        },
                    }}>
                        <TileLayer
                            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                       {this.state.routes.map(route =>
                            <PL  positions={ListPage.decodeGeometry(route.geometry)} color={ListPage.generateRandomPolygonColor()} />
                        )}
                        {this.state.routes.map(route =>
                            route.steps.map( step =>
                                <Marker position={[step.location[1], step.location[0]]}>
                                    <Popup>
                                        {step.description}<br/>
                                        <b>Vehicle:</b> {route.vehicle}<br/>
                                        <b>Type:</b> {step.type}<br/>
                                        <b>Travel time:</b> {ListPage.convertDuration(step.duration)}<br/>
                                        <b>Vehicle Load:</b> {step.load}<br/>
                                    </Popup>
                                </Marker>
                            ))}
                        <LocationMarker effectOn={this}/>
                    </MapContainer>
                </Col>
            </Row>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderContents(this.state.vehicles, this.state.shipments);
            
        return (
            <Container>
                    {contents}
            </Container>
        );
    }

    
    
    
     
    
}