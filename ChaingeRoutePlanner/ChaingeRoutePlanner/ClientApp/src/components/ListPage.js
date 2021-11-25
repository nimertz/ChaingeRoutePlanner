import React, { Component, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup,useMapEvents  } from 'react-leaflet'
import { Button, Row, Col, Container} from 'reactstrap';

function LocationMarker(props) {
    const [position, setPosition] = useState(null)
    const map = useMapEvents({
        click(e) {
            console.log('test', e)

            props.effectOn.setState(state => {
                state.lat = e.latlng.lat;
                state.lng = e.latlng.lng;
                return { ...state }
            });
            //map.locate()
        },
        moveend() {
            let center = map.getCenter();
            let zoom = map.getZoom();

            props.effectOn.setState(state => {
                state.lat = center.lat;
                state.lng = center.lng;
                return { ...state }
            });
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
            
            lat: 35.76218444303944,
            lng: 51.33657932281495,
            checked: false,
            location: [0,0],
            
            vehicles: [],
            shipments: [],
            selectedShipments: [],
            selectedVehicles: [],
        };
    }

    componentDidMount() {
        this.populateLists();
    }

    static renderVehicleList(vehicles) {
        return (
            <ul className="list-group">
                <h5>Vehicles</h5>
                {vehicles.map(vehicle =>
                    <li className="list-group-item">
                        <input className="form-check-input me-1" type="checkbox" value="" aria-label="..."/>
                        {vehicle.description}
                    </li>
                )}
            </ul>
        );
    }

    static renderShipmentList(shipments) {
        return (
            <ul className="list-group">
                <h5>Shipments</h5>
                {shipments.map(vehicle =>
                    <li className="list-group-item">
                        <input className="form-check-input me-1" type="checkbox" value="" aria-label="..."/>
                        {vehicle.description}
                    </li>
                )}
            </ul>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ListPage.renderContents(this.state.vehicles, this.state.shipments);
            
        return (
            <Container>
                    {contents}
            </Container>
        );
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

    static renderContents(vehicles, shipments) {
        let vehicleContent =  ListPage.renderVehicleList(vehicles);
        let shipmentContent = ListPage.renderShipmentList(shipments);
        
        return (
            <Row>
                <Col>
                    {vehicleContent}
                </Col>
                <Col>
                    {shipmentContent}
                    <div>
                        <Button
                            color="primary">
                            Create route plan
                        </Button>
                    </div>
                </Col>
                <Col xs={6}>
                    <MapContainer center={[48, 11]} zoom={10} scrollWheelZoom={true} eventHandlers={{
                        click: () => {
                            console.log('map clicked')
                        },
                    }}>
                        <TileLayer
                            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                        <LocationMarker effectOn={this}/>
                    </MapContainer>
                </Col>
            </Row>
        );
    }

    populateLists() {
        this.populateVehicleData();
        this.populateShipmentData()
        this.setState({ loading: false });
    }
}