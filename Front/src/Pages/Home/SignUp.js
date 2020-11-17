import React, {Fragment, useState} from "react";
import NavBar from "../../Components/NavBar/NavBar";
import LinkWrapper from "../../Utils/LinkWrapper";
import PopUp from "../../Components/PopUp/PopUp";
import ApiClient from "../../Repositories/ApiClient";

export default function({ history }) {
  const [signUpInfo, setSignUpInfo] = useState({ email: "", password: "" });

  function handleInputChange({ target: { name, value } }) {
    setSignUpInfo(prevLoginInfo => ({ ...prevLoginInfo, [name]: value }))
  }

  function createPlayer(e) {
    e.preventDefault();

    ApiClient.CreatePlayer(signUpInfo)
      .then(() => {
          PopUp.showPopUp('success', 'Cadastro realizado com sucesso! Faça o seu login');
          history.push('/Home')
      })
      .catch(() => PopUp.showPopUp('error', 'Falha ao cadastrar'))
  }

  return(
    <Fragment>
      <NavBar />
      <br />
      <div className="container">
        <div className="row">
          <form className="col s12">
            <div className="row">
              <h3 className="center"><b>Realizar Cadastro</b></h3>
            </div>
            <br />
            <div className="row">
              <div className="input-field">
                <div className="input-field">
                  <input
                    placeholder="Nome"
                    type="text"
                    required
                    className="validate"
                    name="name"
                    onChange={handleInputChange} />
                </div>
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
              <LinkWrapper to="/" style={{ color: "white", textDecoration: "underline" }} activeStyle={{}}>
                Realizar login
              </LinkWrapper>
              <button onClick={e => createPlayer(e)} className="waves-effect waves-light darkbg btn-large right">
                Cadastrar
              </button>
            </div>
          </form>
        </div>
      </div>
    </Fragment>
  )
}