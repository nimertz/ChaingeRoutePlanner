import React, { Component, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup,useMapEvents  } from 'react-leaflet'
import { Button, Row, Col, Container, Form } from 'reactstrap';

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
            value: 'Howdy',
            description: '',
            pickup: false,
            location: [0,0],
            amount: '',
            timeStart: '',
            timeEnd: '',
            timeSpan: '',
            lat: 35.76218444303944,
            lng: 51.33657932281495,
            checked: false
        };
    
        this.handleSendData = this.handleSendData.bind(this);
        this.handleCheckbox = this.handleCheckbox.bind(this);
    }

        handleClick = (e) => {
            console.log(e.latlng)
        }

        handleSendData(event) {

            const jsonToSend = {
                "Pickup": this.state.checked,
                "Amount": this.state.amount,
                "Location": [this.state.lat, this.state.lng],
                "Time": [this.state.timeStart, this.state.timeEnd],
                "TimeSpan": this.state.timeSpan
            };

            alert('Data send');
            console.log('stuff', jsonToSend);
            console.log(this.postPackage(this.state.checked, this.state.amount, this.state.lng, this.state.lat, this.state.description))

            //event.preventDefault();
        }

        handleCheckbox(input){
            this.setState({
                checked: !this.state.checked
              });
    }

    postPackage = async (pickup, amount, long, lat, descrip) => {
        const location = window.location.hostname;
        console.log('123', this.state.lon)
        const settings = {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                // your expected POST request payload goes here
                "pickup": pickup,
                "description": descrip,
                "amount": amount,
                "location": [
                    long,
                    lat
                ]
            })

        };
        try {
            const fetchResponse = await fetch(`Shipment`, settings);
            const data = await fetchResponse.json();
            return data;
        } catch (e) {
            return e;
        }

    }

    handleClick = (e) => {
        console.log(e.latlng)
    }

    render() {
        return (
            <Container>
                <Row>
                    <Col>
                        <div className="list-group">
                            <button href="#" className="list-group-item list-group-item-action active" aria-current="true">
                                The current link item
                            </button>
                            <button href="#" className="list-group-item list-group-item-action">A second link item</button>
                            <button href="#" className="list-group-item list-group-item-action">A third link item</button>
                            <button href="#" className="list-group-item list-group-item-action">A fourth link item</button>
                            <button href="#" className="list-group-item list-group-item-action disabled" tabindex="-1" aria-disabled="true">A disabled link item</button>
                        </div>
                    </Col>
                    <Col>
                        <div className="list-group">
                            <button href="#" className="list-group-item list-group-item-action active" aria-current="true">
                                The current link item
                            </button>
                            <button href="#" className="list-group-item list-group-item-action">A second link item</button>
                            <button href="#" className="list-group-item list-group-item-action">A third link item</button>
                            <button href="#" className="list-group-item list-group-item-action">A fourth link item</button>
                            <button href="#" className="list-group-item list-group-item-action disabled" tabindex="-1" aria-disabled="true">A disabled link item</button>
                        </div>
                    </Col>
                    <Col xs={6}>
                    <MapContainer center={[ 48, 11 ]} zoom={10} scrollWheelZoom={true} eventHandlers={{
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
            </Container>
        );
    }
}