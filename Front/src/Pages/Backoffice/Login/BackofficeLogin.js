import React, {useState} from 'react';
import {Card, Button, TextInput, Modal} from 'react-materialize';

import ApiClient from "../../../Repositories/ApiClient";

export default function BackofficeLogin({ history }) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const [loginData, setLoginData] = useState({});

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
    <Card style={{ width: '50rem', padding: 20, margin: 20 }}>
      <h1>Login - Acesso Backoffice</h1>
      <p>Insira seus dados de login para acessar o backoffice</p>

      <form onSubmit={sendLogin}>
        <TextInput
          label="Email"
          placeholder="Seu email"
          onChange={({ target }) =>
            setLoginData({ ...loginData, email: target.value })} />
        <TextInput
          label="Senha"
          placeholder="Sua senha"
          type="password"
          onChange={({ target }) =>
            setLoginData({ ...loginData, email: target.value })} />
        <Button
          type="submit"
          variant="primary"
          disabled={loading}
        >
          {loading ? "Carregando..." : "Entrar"}
        </Button>
        <Modal open={error} onCloseEnd={() => setError(false)} >
          Falha ao fazer login
        </Modal>
      </form>
    </Card>
  )
}