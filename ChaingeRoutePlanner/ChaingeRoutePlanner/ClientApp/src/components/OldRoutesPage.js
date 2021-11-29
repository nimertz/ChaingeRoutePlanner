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
            center: [55.66064229583371, 12.59125202894211]
        };

        this.getData = this.getData.bind(this);
    }

    handleClick = (e) => {
        console.log(e.latlng)
    }

    handleRouteSelect(route) {
        this.setState({ selectedRoutes: route });
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
            <ul className="list-group">
                <h5>Routes</h5>
                {routes.map((routes, index) =>
                    <li className="list-group-item" key={index + 'routeID'}>
                        <Input onChange={() => this.handleRouteSelect(routes.routes)} className="form-check-input me-1 changie_checkBox" type="checkbox" value="Vehicle" aria-label="..." />
                        {routes.routes[0].distance + " / " + routes.routes[0].duration}
                    </li>
                )}
            </ul>
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