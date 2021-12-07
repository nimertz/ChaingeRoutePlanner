import React, { Component } from 'react';
import { Row, Col, Container, Input, Label, Button, Form, FormGroup } from 'reactstrap';
import bikeImg from "../assets/Bike.PNG"
import {Redirect} from "react-router-dom";

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
            this.postBike(this.state.bikeDescription, this.state.bikeCapacity)
            alert("Bike added")

            //event.preventDefault();
    }

    async populateWeatherData() {
        const response = await fetch('Vehicle/all');
        const data = await response.json();
        console.log("data", data);
    }

    postBike = async (description, capacity) => {
        const settings = {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "description": description,
                "capacity": capacity,
                //"time_window": [28800,57600]
            })

        };
        try {
            const fetchResponse = await fetch(`Vehicle`, settings);
            const data = await fetchResponse.json();
            //this.setState({ bikeDescription: data });
            document.getElementById("bikeDescriptionID").reset();

            return data;
        } catch (e) {
            return e;
        }

    }
    

    render() {
        return (
            <Container>
                <Row>
                    <Col>
                    <Form>
                      
                    <FormGroup>
                        <Col>
                            <Label className="label_text" >Description</Label>
                        </Col>
                        <Col>
                                <Input onChange={event => this.state.bikeDescription = event.target.value} type="text" className="form-control" id="bikeDescriptionID" aria-describedby="emailHelp" placeholder="Description" />
                        </Col>
                    </FormGroup>
                        <FormGroup row>
                            <Label className="label_text">Capacity</Label>
                            <Col xs={11}>
                    <Input onChange={event => this.state.bikeCapacity = event.target.value} type="number" name='Capacity' id="" placeholder="Bike capacity" />
                                </Col>
                                <Col>
                                        <label className="label_text">Kg</label>
                                </Col>
                    </FormGroup>
                    <FormGroup>
                        <Label className="label_text">Active Date</Label>
                          <Input type="date" name='date_of_delivery'  onChange={event => this.state.data = event.target.value}/>
                    </FormGroup>
                    <FormGroup row>
                                <Label className="label_text">Active Timeroom</Label>
                                <Col>
                        <Label className="label_text">Time - Start</Label>
                                    <Input type="time" name='delivery_clock_start' onChange={event => this.state.timeStart = event.target.value} />
                                </Col>
                                <Col>
                        <Label className="label_text">Time - end</Label>
                                    <Input type="time" name='delivery_clock_end' onChange={event => this.state.timeEnd = event.target.value} />
                                    </Col>
                    </FormGroup>
                    <FormGroup>
                        <Label className="label_text">Max Tasks</Label>
                        <Input onChange={event => this.state.maxTasks = event.target.value} type="number" name='date_of_birth'  />
                    </FormGroup>
                    <br/>
                    <Button onClick={this.handleSendData} className="btn chainge-color button-chainge">Add Bike</Button>
                        </Form>
                    </Col>
              
                </Row>
                    
            </Container>
        );
    }
}