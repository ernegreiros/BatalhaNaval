import React, { Fragment, useState } from 'react';
import { Card, Button, Modal } from 'react-materialize';
import NavBar from '../../../Components/NavBar/NavBar';

import ApiClient from "../../../Repositories/ApiClient";
import UserService from "../../../Services/UserService";

export default function BackofficeLogin({ history }) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [loginData, setLoginData] = useState({ Login: null, Password: null });

  function sendLogin(event) {
    event.preventDefault()

    setLoading(true);
    setError(false);

    ApiClient.AdminLogin(loginData)
      .then(() => history.push('/backoffice'))
      .catch(() => {
        setLoading(false)
        setError(true)
      });
  }
  return (
    <Fragment>
      <NavBar />
      <div className="row">
        

        <div className='col l6 push-l3'>
          <br />
          <Card style={{ width: '50rem' }} className="medium">

            <h3 className="center">Login - Acesso Backoffice</h3>

            <br />

            <div className="row center">
              <h6>Insira seus dados de login para acessar o backoffice</h6>
            </div>

            <br />
            <div className="row">
              <form onSubmit={sendLogin} className="col s12">
                <div className="row ">

                  <div className="input-field col s6">
                    <input
                      id="email"
                      type="text"
                      className="validate"
                      onChange={({ target }) =>
                        setLoginData({ ...loginData, Login: target.value })} />
                    <label htmlFor="email">Email</label>
                  </div>

                  <div className="input-field col s6">
                    <input
                      className="validate"
                      type="password"
                      id="senha"
                      onChange={({ target }) =>
                        setLoginData({ ...loginData, Password: target.value })} />
                    <label htmlFor="senha">Senha</label>
                  </div>
                </div>
                <div className="row">
                  <Button
                    type="submit"
                    variant="primary"
                    disabled={loading}
                    className="right large"
                  >
                    {loading ? "Carregando..." : "Entrar"}
                  </Button>
                </div>
                <Modal open={error} onCloseEnd={() => setError(false)} >
                  Falha ao fazer login
              </Modal>
              </form>
            </div>
          </Card>
        </div>

        
      </div>
    </Fragment>
  )
}