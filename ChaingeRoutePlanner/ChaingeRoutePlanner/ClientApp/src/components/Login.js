import React, { Component } from 'react';
import { Button, Row, Col, Container, NavLink, Input } from 'reactstrap';

import { Link } from 'react-router-dom';
import './NavMenu.css';
import logo from "../assets/ChaingeLogo.png"

export class Login extends Component {
    static displayName = Login.name;

    render() {
        return (
            <div>
                <br/>
                <Row className="justify-content-md-center">
                    <img src={logo} style={{ width: '30vw', marginTop: -7 }} />
                </Row>
                <br />
                <Row>
                    <Col xs={2}>
                    </Col>
                    <Col xs={2}>
                        <label>Username</label>
                    </Col>
                    <Col xs={6}>
                        <Input type="text" name='Capacity' id="" placeholder="Username" />
                    </Col>
                </Row>
                <br/>
                <Row>
                    <Col xs={2}>
                    </Col>
                    <Col xs={2}>
                        <label>Password</label>
                    </Col>
                    <Col xs={6}>
                        <Input type="password" name='Capacity' id="" placeholder="Password" />
                    </Col>
                </Row>
                <br/>
                <Row>
                    <Col xs={2}>
                    </Col>
                    <Col xs={8}>
                        <Link to="/home">
                            <Button value="test" className="chainge-color" style={{ width: '100%', height: '40px' }}>
                                Login
                            </Button>
                        </Link>
                    </Col>
                </Row>
            </div>
        );
    }
}