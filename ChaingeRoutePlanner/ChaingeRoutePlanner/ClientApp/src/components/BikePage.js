import React, { Component } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import { Button, Row, Col, Container, Form } from 'reactstrap';

export class BikePage extends Component {
    static displayName = BikePage.name;
    constructor(props) {
        super(props);
        this.state = {
            value: 'Howdy',
            bikeDescription: '',
            bikeCapacity: '',
            timeStart: '',
            timeEnd: '',
            maxTasks: '',
            date: ''
        };
    
        this.handleSendData = this.handleSendData.bind(this);
        this.populateWeatherData = this.populateWeatherData.bind(this);
    }

        handleClick = (e) => {
            console.log(e.latlng)
        }

        handleSendData(event) {

            const jsonToSend = {
                "BikeDescription": this.state.bikeDescription,
                "Capacity": this.state.bikeCapacity,
                "Date": this.state.date,
                "Time": [this.state.timeStart, this.state.timeEnd],
                "MaxTasks": this.state.maxTasks
            };

            console.log('stuff', jsonToSend);
            this.populateWeatherData();
            console.log('getDvic', this.postBike(this.state.bikeDescription, this.state.bikeCapacity));

            //event.preventDefault();
    }

    async populateWeatherData() {
        console.log('test');
        const response = await fetch('Vehicle/all');
        const data = await response.json();
        console.log(data);
    }

    postBike = async (description, capa) => {
        const location = window.location.hostname;
        const settings = {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                // your expected POST request payload goes here
                "description": description,
                "capacity": capa
            })

        };
        try {
            const fetchResponse = await fetch(`Vehicle`, settings);
            const data = await fetchResponse.json();
            return data;
        } catch (e) {
            return e;
        }

    }
    

    render() {
        return (
            <Container>
                <Row>
                            <div className="form-group">
                                <Col>
                                    <label>Description</label>
                                </Col>
                                <Col>
                                    <input onChange={event => this.state.bikeDescription = event.target.value} type="text" className="form-control" id="bikeDescriptionID" aria-describedby="emailHelp" placeholder="Bike Description" />
                                </Col>
                            </div>
                            <div className="form-group">
                                <label>Capacity</label>
                                <div>
                            <input onChange={event => this.state.bikeCapacity = event.target.value} type="number" name='Capacity' id="" placeholder="Bike Description" />
                                </div>
                            </div>
                            <div className="form-group">
                                <label>Active Timeroom</label>
                                <label>Date</label>
                                  <input type="date" name='date_of_delivery'  onChange={event => this.state.data = event.target.value}/>
                            </div>
                            <div className="form-group">
                                <label>Active Timeroom</label>
                                <label>Time - Start</label>
                                <input type="time" name='delivery_clock_start'  onChange={event => this.state.timeStart = event.target.value}/>
                                <label>Time - end</label>
                                <input type="time" name='delivery_clock_end'  onChange={event => this.state.timeEnd = event.target.value}/>
                            </div>
                            <div className="form-group">
                                <label >Max Tasks</label>
                                 <input onChange={event => this.state.maxTasks = event.target.value} type="number" name='date_of_birth'  />
                            </div>
                            <button onClick={this.handleSendData} className="btn btn-primary">Add Bike</button>
                </Row>
            </Container>
        );
    }
}