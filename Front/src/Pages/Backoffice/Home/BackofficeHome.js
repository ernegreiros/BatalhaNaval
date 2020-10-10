import React from "react";
import {Link, Route} from "react-router-dom";
import { Nav, Navbar } from "react-bootstrap";

import './BackofficeHome.css';

import BackofficeSpecialPowers from "../SpecialPowers/BackofficeSpecialPowers";

export default function BackofficeHome({ match }) {
  return (
    <div>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand>Batalha Naval - Backoffice</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link as={Link} to="/backoffice/special-powers">Super Poderes</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>

      <Route path={`${match.path}/`} exact={true}>
        <h1>Backoffice Home</h1>
      </Route>

      <Route path={`${match.path}/special-powers`} component={BackofficeSpecialPowers} />
    </div>
  )
}