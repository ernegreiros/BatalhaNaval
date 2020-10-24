import React, {useEffect, useState} from "react";
import {Table, Preloader, Row} from 'react-materialize';

import ApiClient from "../../../Repositories/ApiClient";

export default function BackofficeThemes() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [themes, setThemes] = useState([]);

  useEffect(() => getSpecialPowers(), []);

  function getSpecialPowers() {
    setError(false);
    setLoading(true);
    ApiClient.GetThemes()
      .then(({ themes }) => setThemes(themes))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  function renderTable() {
    return (
      <Table style={{ margin: 20 }}>
        <thead>
        <tr>
          <th>#</th>
          <th>Nome</th>
          <th>Descrição</th>
        </tr>
        </thead>
        <tbody>
          {themes.map(({ id, name, description }) => {
            return (
              <tr key={id}>
                <td>{id}</td>
                <td>{name}</td>
                <td>{description}</td>
              </tr>
            )
          })}
        </tbody>
      </Table>
    )
  }

  return (
    <div>
      <h4>Cadastros - Temas</h4>
      <br />

      {error
        ? <p>Falha ao carregar poderes especiais do servidor</p>
        : loading
          ? <Row style={{ marginTop: 20 }}><Preloader /></Row>
          : themes.length === 0
            ? <p>Não há nenhum tema cadastrado</p>
            : renderTable()
      }
    </div>
  )
}