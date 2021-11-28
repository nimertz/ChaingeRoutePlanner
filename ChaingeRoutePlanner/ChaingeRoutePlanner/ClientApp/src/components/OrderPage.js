import React, {Component, useState} from 'react';
import {MapContainer, Marker, Popup, TileLayer, useMapEvents} from 'react-leaflet'
import {Button, Col, Container, Input, Label, Row, Form, FormGroup} from 'reactstrap';

function LocationMarker(props) {
    const [position, setPosition] = useState(null)
    const map = useMapEvents({
        click(e) {
            console.log('location', e)

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
            description: '',
            pickup: false,
            location: [0,0],
            amount: '',
            window_start: 0,
            window_end: 0,
            timeSpan: '',
            lat: 35.76218444303944,
            lng: 51.33657932281495,
            checked: false
        };
    
        this.handleSendData = this.handleSendData.bind(this);
        this.handleCheckbox = this.handleCheckbox.bind(this);
    }

    handleSendData(event) {

        const jsonToSend = {
            "Pickup": this.state.checked,
            "Amount": this.state.amount,
            "Location": [this.state.lat, this.state.lng],
            "Time_windows": [this.state.window_start, this.state.window_end],
            "TimeSpan": this.state.timeSpan
        };

        alert('Data send');
        console.log('Json values', jsonToSend);
        console.log(this.postPackage(this.state.checked, this.state.amount, this.state.lng, this.state.lat, this.state.description, this.state.window_start, this.state.window_end));

        //event.preventDefault();
    }

    handleCheckbox(input){
        this.setState({
            checked: !this.state.checked
          });
    }       

    postPackage = async (pickup, amount, long, lat, desc,timeStart,timeEnd) => {
        //exclude timeStart and timeEnd if not set
        let settings;
        if(timeStart === 0 || timeEnd === 0){
             settings = {
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    "pickup": pickup,
                    "description": desc,
                    "amount": amount,
                    "location": [
                        long,
                        lat
                    ]
                })
        }
        } else {
            settings = {
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    "pickup": pickup,
                    "description": desc,
                    "amount": amount,
                    "location": [
                        long,
                        lat
                    ],
                    "time_windows": [
                        [
                            timeStart,
                            timeEnd
                        ]
                    ]
                })
            }
        }
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
    
    static convertDateTimeToInt64(value) {
        //convert date time to UTC +1
        let date = new Date(value);
        date.setHours(date.getHours() + 1);
        return parseInt((date.getTime() / 1000).toFixed(0))
    }

    render() {
        return (
            <Container>
                <Row>
                    <Col>
                        <Form>
                            <h1>Order Shipment</h1>
                            <FormGroup row>
                                    <Label>Shipment type:</Label>
                                    <Input onChange={event => this.handleCheckbox(event.target.value)} checked={!this.state.checked}  type="checkbox" value="Delivery" />Delivery
                                    <Input  onChange={event => this.handleCheckbox(event.target.value)} checked={this.state.checked} type="checkbox" value="Pickup" />Pickup
                            </FormGroup>
                            <FormGroup>
                                <Label >Description</Label>
                                <Input onChange={event => this.state.description = event.target.value} type="text" name='Name' />
                            </FormGroup>
                            <FormGroup>
                                <Label >Weight</Label>
                                <Input onChange={event => this.state.amount = event.target.value} type="number" name='amount'  />
                            </FormGroup>
                            <FormGroup>
                                <Label >Location</Label>
                                <Input value={this.state.lat} className="form-control" id="lat" disabled={true}/>
                                <Input value={this.state.lng} className="form-control" id="lng" disabled={true}/>
                            </FormGroup>
                            <FormGroup>
                                <h4>Time Window</h4>
                                <Label >Between :</Label>
                                <Input  onChange={event => this.state.window_start = OrderPage.convertDateTimeToInt64(event.target.value)} type="datetime-local" name='time_start'  />
                                <Label >And</Label>
                                <Input  onChange={event => this.state.window_end = OrderPage.convertDateTimeToInt64(event.target.value)} type="datetime-local" name='time_end'  />
                            </FormGroup>
                            <Button onClick={this.handleSendData} className="btn btn-primary">Add Order</Button>
                        </Form>
                    </Col>
                    <Col>
                    <MapContainer center={[ 55.66064229583371, 12.59125202894211 ]} zoom={10} scrollWheelZoom={true} eventHandlers={{
                        click: () => {
                        console.log('map clicked')
                        },
                    }}>
                            <TileLayer
                                attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                            <LocationMarker effectOn={this}/>
                            <Marker position={[ this.state.lat, this.state.lng ]}>
                                <Popup>
                                   Location of pickup or delivery
                                </Popup>
                            </Marker>
                        </MapContainer>
                    </Col>
                </Row>
            </Container>
        );
    }
}