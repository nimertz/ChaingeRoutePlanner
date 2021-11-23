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

            alert('Data send');
            console.log('stuff', jsonToSend);

            //event.preventDefault();
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
                                    <Form.Control onChange={event => this.state.bikeCapacity = event.target.value} type="number" name='Capacity' id="" placeholder="Bike Description" />
                                </div>
                            </div>
                            <div className="form-group">
                                <label>Active Timeroom</label>
                                <label>Date</label>
                                <Form.Control type="date" name='date_of_delivery'  onChange={event => this.state.data = event.target.value}/>
                            </div>
                            <div className="form-group">
                                <label>Active Timeroom</label>
                                <label>Time - Start</label>
                                <Form.Control type="time" name='delivery_clock_start'  onChange={event => this.state.timeStart = event.target.value}/>
                                <label>Time - end</label>
                                <Form.Control type="time" name='delivery_clock_end'  onChange={event => this.state.timeEnd = event.target.value}/>
                            </div>
                            <div className="form-group">
                                <label >Max Tasks</label>
                                <Form.Control onChange={event => this.state.maxTasks = event.target.value} type="number" name='date_of_birth'  />
                            </div>
                            <button onClick={this.handleSendData} className="btn btn-primary">Add Bike</button>
                </Row>
            </Container>
        );
    }
}