import React, { Component } from 'react';
import {Row, Col, Container, Input, Label, Button, Form, FormGroup} from 'reactstrap';

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
        const response = await fetch('Vehicle/all');
        const data = await response.json();
        console.log("data", data);
    }

    postBike = async (description, capa) => {
        const settings = {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
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
                    <Form>
                    <FormGroup>
                        <Col>
                            <Label>Description</Label>
                        </Col>
                        <Col>
                            <Input onChange={event => this.state.bikeDescription = event.target.value} type="text" className="form-control" id="bikeDescriptionID" aria-describedby="emailHelp" placeholder="Description" />
                        </Col>
                    </FormGroup>
                    <FormGroup>
                        <Label>Capacity</Label>
                        <div>
                    <Input onChange={event => this.state.bikeCapacity = event.target.value} type="number" name='Capacity' id="" placeholder="Bike capacity (kg)" />
                        </div>
                    </FormGroup>
                    <FormGroup>
                        <Label>Active Timeroom</Label>
                        <Label>Date</Label>
                          <Input type="date" name='date_of_delivery'  onChange={event => this.state.data = event.target.value}/>
                    </FormGroup>
                        <FormGroup>
                        <Label>Active Timeroom</Label>
                        <Label>Time - Start</Label>
                        <Input type="time" name='delivery_clock_start'  onChange={event => this.state.timeStart = event.target.value}/>
                        <Label>Time - end</Label>
                        <Input type="time" name='delivery_clock_end'  onChange={event => this.state.timeEnd = event.target.value}/>
                    </FormGroup>
                    <FormGroup>
                        <Label >Max Tasks</Label>
                        <Input onChange={event => this.state.maxTasks = event.target.value} type="number" name='date_of_birth'  />
                    </FormGroup>
                    <Button onClick={this.handleSendData} className="btn btn-primary">Add Bike</Button>
                    </Form>
                </Row>
            </Container>
        );
    }
}