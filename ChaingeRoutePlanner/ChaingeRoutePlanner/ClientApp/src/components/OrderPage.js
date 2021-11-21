import React, { Component, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup,useMapEvents  } from 'react-leaflet'
import { Button, Row, Col, Container } from 'react-bootstrap';
import TimePicker from 'react-time-picker';
import Form from "react-bootstrap/Form";

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

export class OrderPage extends Component {
    static displayName = OrderPage.name;
    constructor(props) {
        super(props);
        this.state = {
            value: 'Howdy',
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

            //event.preventDefault();
        }

        handleCheckbox(input){
            this.setState({
                checked: !this.state.checked
              });
        }

    handleClick = (e) => {
        console.log(e.latlng)
    }

    render() {
        return (
            <Container>
                <Row>
                    <Col>
                            <div className="form-group">
                                <Col>
                                    <label >Pickup</label>
                                </Col>
                                <Col>
                                    <input  onChange={event => this.handleCheckbox(event.target.value)} checked={this.state.checked} type="checkbox" value="Pickup" />Pickup
                                    <br/>
                                    <input onChange={event => this.handleCheckbox(event.target.value)} checked={!this.state.checked}  type="checkbox" value="Delivery" />Delivery
                                </Col>
                            </div>
                            <div className="form-group">
                                <label >Amount</label>
                                <Form.Control onChange={event => this.state.amount = event.target.value} type="number" name='amount'  />
                            </div>
                            <div className="form-group">
                                <label >Location</label>
                                <input value={this.state.lat} className="form-control" id="exampleInputPassword1" disabled={true}/>
                                <input value={this.state.lng} className="form-control" id="exampleInputPassword1" disabled={true}/>
                            </div>
                            <div className="form-group">
                                <label >Time - Start</label>
                                <Form.Control  onChange={event => this.state.timeStart = event.target.value} type="time" name='time_start'  />
                                <label >Time - end</label>
                                <Form.Control  onChange={event => this.state.timeEnd = event.target.value} type="time" name='time_end'  />
                            </div>
                            <div className="form-group">
                                <label >Timespan</label>
                                <Form.Control onChange={event => this.state.timeSpan = event.target.value} type="number" name='date_of_birth'  />
                            </div>
                            
                            <button onClick={this.handleSendData} className="btn btn-primary">Add Order</button>
                    </Col>
                    <Col>
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