import React, {Fragment, useState} from "react";
import NavBar from "../../Components/NavBar/NavBar";
import LinkWrapper from "../../Utils/LinkWrapper";
import PopUp from "../../Components/PopUp/PopUp";
import ApiClient from "../../Repositories/ApiClient";

export default function({ history }) {
  const [loginInfo, setLoginInfo] = useState({ login: "", password: "" });

  function handleInputChange({ target: { name, value } }) {
    setLoginInfo(prevLoginInfo => ({ ...prevLoginInfo, [name]: value }))
  }

  function playerLogin(e) {
    e.preventDefault();

    ApiClient.Login(loginInfo)
      .then(() => {
        //PopUp.showPopUp('success', 'Login realizado com sucesso! Boa diversão!'); //api esta deixando logar sem um user por algum motivo
        history.push('/Home')
      })
      .catch(() => PopUp.showPopUp('error', 'Falha ao realiar login'))
  }

  return(
    <Fragment>
      <NavBar />
      <br />
      <div className="container">
        <div className="row">
          <form className="col s12">
            <div className="row">
              <h3 className="center"><b>Realizar Login</b></h3>
            </div>
            <br />
            <div className="row">
              <div className="input-field">
                <input
                  placeholder="Login"
                  type="text"
                  required
                  className="validate"
                  name="login"
                  onChange={handleInputChange} />
              </div>
              <div className="input-field">
                <input
                  placeholder="Senha"
                  type="password"
                  required
                  name="password"
                  className="validate"
                  onChange={handleInputChange} />
              </div>
            </div>
            <div className="row">
              <LinkWrapper to="/signup" style={{ color: "white", textDecoration: "underline" }} activeStyle={{}}>
                Realizar cadastro
              </LinkWrapper>
              <button onClick={e => playerLogin(e)} className="waves-effect waves-light darkbg btn-large right">
                Entrar
              </button>
            </div>
          </form>
        </div>
      </div>
    </Fragment>
  )
}