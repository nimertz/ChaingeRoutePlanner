import React, { Component } from 'react';
import { Button, Row, Col, Container, NavLink, NavbarBrand } from 'reactstrap';

import { Link } from 'react-router-dom';
import './NavMenu.css';
import logo from "../assets/ChaingeLogo.png"
import bike from "../assets/bikeIcon.png"
import arch from "../assets/ArchIcon.png"
import packageIcon from "../assets/packageIcon.png"
import calc from "../assets/CalcIcon.png"

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <Row>
                <Col>
                    <div style={{ textAlign: 'center' }}>
                        <NavLink tag={Link} className="chainge-title" to="/listPage">Plan routes</NavLink>
                        <NavbarBrand tag={Link} to="/listPage"><img src={calc} style={{ height: '20vh', marginTop: -7 }} /></NavbarBrand>

                    </div>
                </Col>
                <Col>
                    <div style={{ textAlign: 'center' }}>
                        <NavLink tag={Link} className="chainge-title" to="/orderPage">Add Order</NavLink>
                        <NavbarBrand tag={Link} to="/orderPage"><img src={packageIcon} style={{ height: '20vh', marginTop: -7 }} /></NavbarBrand>
                    </div>
                </Col>
                <Col>
                    <div style={{ textAlign: 'center' }}>
                        <NavLink tag={Link} className="chainge-title" to="/bikePage">Add Bike</NavLink>
                        <NavbarBrand tag={Link} to="/bikePage"><img src={bike} style={{ height: '20vh', marginTop: -7 }} /></NavbarBrand>
                    </div>
                </Col>
                <Col>
                    <div style={{ textAlign: 'center' }}>
                        <NavLink tag={Link} className="chainge-title" to="/oldroute">Old Routes</NavLink>
                        <NavbarBrand tag={Link} to="/oldroute"><img src={arch} style={{ height: '20vh', marginTop: -7 }} /></NavbarBrand>
                    </div>
                </Col>
         </Row>
      </div>
    );
  }
}
