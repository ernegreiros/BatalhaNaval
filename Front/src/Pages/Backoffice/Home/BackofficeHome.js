import React, { Component, Fragment } from "react";
import M from "materialize-css";
import './BackofficeHome.css';
import BackofficeSpecialPowers from "../SpecialPowers/BackofficeSpecialPowers";
import NavBar from "../../../Components/NavBar/NavBar";
import BackofficeThemes from "../Themes/BackofficeThemes";

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
              <li>
                <div className="collapsible-header"><i className="material-icons">whatshot</i>Poderes Especiais</div>
                <div className="collapsible-body">
                  <BackofficeSpecialPowers />
                </div>
              </li>
              <li>
                <div className="collapsible-header"><i className="material-icons">format_paint</i>Temas</div>
                <div className="collapsible-body">
                  <BackofficeThemes />
                </div>
              </li>
            </ul>
          </div>
        </div>
      </Fragment>

    )
  }
}

export default BackofficeHome;