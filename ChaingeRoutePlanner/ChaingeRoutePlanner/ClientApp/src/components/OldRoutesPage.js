import React, { Component } from 'react';
import { Row, Col, Container, Input, Label, Button, Form, FormGroup } from 'reactstrap';

export class OldRoutesPage extends Component {
    static displayName = OldRoutesPage.name;
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

        this.getData = this.getData.bind(this);
    }

    handleClick = (e) => {
        console.log(e.latlng)
    }

    async getData(event) {

        const response = await fetch('RoutePlan/all');
        const data = await response.json();
        console.log("data", data);

        //event.preventDefault();
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
                            <Label className="">Capacity</Label>
                            <div>
                                <Input onChange={event => this.state.bikeCapacity = event.target.value} type="number" name='Capacity' id="" placeholder="Bike capacity (kg)" />
                            </div>
                        </FormGroup>
                        <FormGroup>
                            <Label>Active Timeroom</Label>
                            <Label>Date</Label>
                            <Input type="date" name='date_of_delivery' onChange={event => this.state.data = event.target.value} />
                        </FormGroup>
                        <FormGroup>
                            <Label>Active Timeroom</Label>
                            <Label>Time - Start</Label>
                            <Input type="time" name='delivery_clock_start' onChange={event => this.state.timeStart = event.target.value} />
                            <Label>Time - end</Label>
                            <Input type="time" name='delivery_clock_end' onChange={event => this.state.timeEnd = event.target.value} />
                        </FormGroup>
                        <FormGroup>
                            <Label >Max Tasks</Label>
                            <Input onChange={event => this.state.maxTasks = event.target.value} type="number" name='date_of_birth' />
                        </FormGroup>
                        <br />
                        <Button onClick={this.getData} className="btn chainge-color">Add Bike</Button>
                    </Form>
                </Row>
            </Container>
        );
    }
}