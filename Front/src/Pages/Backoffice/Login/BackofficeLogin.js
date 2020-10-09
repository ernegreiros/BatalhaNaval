import React, {useState, useRef} from 'react';
import {Card, Form, Button, Alert} from "react-bootstrap";
import ApiClient from "../../../Repositories/ApiClient";

export default function BackofficeLogin({ history }) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);
  const emailInputRef = useRef(null);
  const passwordInputRef = useRef(null);

  function sendLogin(event) {
    event.preventDefault()

    setLoading(true);
    setError(false);

    const requestData = { email: emailInputRef.current.value, password: passwordInputRef.current.value }

    ApiClient.AdminLogin(requestData)
      .then(() => history.push('/backoffice'))
      .catch(() => {
        setLoading(false)
        setError(true)
      });
  }
  return (
    <Card style={{ width: '50rem', padding: 20, margin: 20 }}>
      <Card.Body>
        <Card.Title>Login - Acesso Backoffice</Card.Title>
        <Card.Text>Insira seus dados de login para acessar o backoffice</Card.Text>

        <Form onSubmit={sendLogin}>
          <Form.Group controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control ref={emailInputRef} type="email" required placeholder="Seu email" />
          </Form.Group>
          <Form.Group controlId="password">
            <Form.Label>Senha</Form.Label>
            <Form.Control ref={passwordInputRef} type="password" required placeholder="Sua senha" />
          </Form.Group>
          <Button
            type="submit"
            variant="primary"
            disabled={loading}
          >
            {loading ? "Carregando..." : "Entrar"}
          </Button>
          {error && <Alert variant="danger" style={{ marginTop: 10 }}>Falha ao enviar</Alert>}
        </Form>
      </Card.Body>
    </Card>
  )
}