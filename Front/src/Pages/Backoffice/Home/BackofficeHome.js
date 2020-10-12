import React from "react";
import {Route} from "react-router-dom";
import {Navbar, NavItem, Icon} from "react-materialize";

import './BackofficeHome.css';

import BackofficeSpecialPowers from "../SpecialPowers/BackofficeSpecialPowers";

export default function BackofficeHome({ match }) {
  return (
    <div>
      <Navbar
        alignLinks="right"
        brand={<a href="/backoffice">Balha Naval - Backoffice</a>}
        menuIcon={<Icon>menu</Icon>}
        options={{
          draggable: true,
          edge: 'left',
          inDuration: 250,
          onCloseEnd: null,
          onCloseStart: null,
          onOpenEnd: null,
          onOpenStart: null,
          outDuration: 200,
          preventScrolling: true
        }}
      >
        <NavItem href="/backoffice/special-powers">Poderes especiais</NavItem>
      </Navbar>

      <Route path={`${match.path}/`} exact={true}>
        <h1>Batalha Naval - Backoffice Administrativo</h1>
      </Route>

      <Route path={`${match.path}/special-powers`} component={BackofficeSpecialPowers} />
    </div>
  )
}