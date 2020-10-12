import React, { Component, Fragment } from "react";
import M from "materialize-css";
import './BackofficeHome.css';
import BackofficeSpecialPowers from "../SpecialPowers/BackofficeSpecialPowers";
import NavBar from "../../../Components/NavBar/NavBar";

class BackofficeHome extends Component {

  componentDidMount() {
    M.AutoInit();
  }

  render() {
    return (
      <Fragment>
        <NavBar />
        <div className="container">
          <div className="row">
            <h4>Backoffice Administrativo</h4>
          </div>
          <div className="row">
            <ul className="collapsible">
              <li className="">
                <div className="collapsible-header"><i className="material-icons">whatshot</i>Poderes Especiais</div>
                <div className="collapsible-body">
                  <BackofficeSpecialPowers />
                </div>
              </li>
              <li className="">
                <div className="collapsible-header"><i className="material-icons">person</i>Usuários</div>
                <div className="collapsible-body"><span>Em Breve...</span></div>
              </li>
            </ul>
          </div>
        </div>
      </Fragment>

    )
  }
}

export default BackofficeHome;