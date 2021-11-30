import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { NavLink as RRNavLink } from 'react-router-dom';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import logo from "../assets/ChaingeLogo.png"

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
                    <NavbarBrand tag={Link} to="/home"><img src={logo} style={{ width: 100, marginTop: -7 }} /></NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="chainge-text" activeClassName="nav-selected" tag={RRNavLink} to="/listPage">Plan routes</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="chainge-text" activeClassName="nav-selected" tag={RRNavLink} to="/orderPage">Add Order</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="chainge-text" activeClassName="nav-selected" tag={RRNavLink} to="/bikePage">Add Bike</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="chainge-text" activeClassName="nav-selected" tag={RRNavLink} to="/oldroute">Old Routes</NavLink>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
