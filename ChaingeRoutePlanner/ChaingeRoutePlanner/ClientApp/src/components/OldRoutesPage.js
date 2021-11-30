import React, { Component } from 'react';
import { Row, Col, Container, Input, Label, Button, Form, FormGroup } from 'reactstrap';
import { Map } from './Map';

export class OldRoutesPage extends Component {
    static displayName = OldRoutesPage.name;
    constructor(props) {
        super(props);
        this.state = {
            routes: [],
            selectedRoutes: [],
            selectedR: null,
            center: [55.66064229583371, 12.59125202894211],
            selectedOption: null
        };

        this.getData = this.getData.bind(this);
    }

    handleClick = (e) => {
        console.log(e.latlng)
    }

    handleRouteSelect(route, index) {
        console.log(route);
        this.setState({ selectedRoutes: route, selectedR: index });
    }

    async getData(event) {

        const response = await fetch('RoutePlan/all');
        const data = await response.json();
        console.log("data", data);
        this.setState({ routes: data });
        console.log(this.state.routes);
        //event.preventDefault();
    }

    routesRenderList(routes) {
        return (
            <Container>
                <Row>
                    <h5>Old Routes</h5>'
                </Row>
                <Row>
                    <Col>
                        <Input type="date" name='date_of_delivery' onChange={event => this.state.data = event.target.value} />
                    </Col>
                    <Col>
                        <Input type="select" name="select" id="exampleSelect" value={this.state.selectedOption} onChange={event => this.setState({ selectedOption: event.target.value})}>
                            <option value={null}>All</option>
                            <option value={0}>1</option>
                            <option value={1}>2</option>
                            <option value={2}>3</option>
                        </Input>
                    </Col>
                </Row>
                <br/>
                <ul className="list-group">
                {routes.map((routes, index) =>
                (index % 3 == this.state.selectedOption || null == this.state.selectedOption)  && <li className="list-group-item" key={index + 'routeID'}>
                        <Input onClick={() => this.handleRouteSelect(routes.routes, index)} className="form-check-input me-1 changie_checkBox" type="button" value={'Bike ' + (1 + index % 3) + " / " + 'Bike ' + (1 + index % 3) } aria-label="..." style={{ width: '100%', height: '100%', backgroundColor: index == this.state.selectedR ? 'rgb(105, 219, 26)' : '', color: index == this.state.selectedR ? ' white' : '', fontWeight: 'bold'}} >
                        </Input>
                        <div style={{ textAlign: 'center' }}>
                            <label style={{ fontSize: '10px', color: Object.keys(this.state.routes).length / 2 > index ? 'black' : 'rgb(105, 219, 26)' }}>
                                {
                                    Object.keys(this.state.routes).length/2 > index ? 'Delivered' : 'Active'
                                 }
                            </label>
                        </div>
                    </li>
                )}
                    </ul>
            </Container>
        );
    }

    componentDidMount() {
        this.getData();
    }


    render() {
        let vehicleContent = this.routesRenderList(this.state.routes);

        return (
            <Container>
                <Row>
                    <Col>
                        {vehicleContent}
                    </Col>
                    <Col>
                        <Map center={this.state.center} routes={this.state.selectedRoutes} height={"65vh"} />
                    </Col>
                </Row>
            </Container>
        );
    }
}