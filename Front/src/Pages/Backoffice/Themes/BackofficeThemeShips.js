import React, {useEffect, useState} from "react";
import {Preloader, Row, Table} from "react-materialize";
import ApiClient from "../../../Repositories/ApiClient";
import ShipsTypes from "../../../Enums/ShipsTypes";

export function BackofficeThemeShips({ themeId }) {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [ships, setShips] = useState([]);

  useEffect(() => getShips(), [])

  function getShips() {
    setError(false);
    setLoading(true);
    ApiClient.GetThemeShips(themeId)
      .then(({ ships }) => setShips(ships))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  function mapTypeName(type) {
    const { OneField, TwoFields, ThreeFields, FourFields, FiveFields } = ShipsTypes;
    const names = {
      [OneField]: "1",
      [TwoFields]: "2",
      [ThreeFields]: "3",
      [FourFields]: "4",
      [FiveFields]: "5",
    }
    return names[type];
  }

  function renderTable() {
    return (
      <Table style={{ margin: 20 }}>
        <thead>
        <tr>
          <th>#</th>
          <th>Nome</th>
          <th>Campos</th>
          <th>Ações</th>
        </tr>
        </thead>
        <tbody>
        {ships.map(ship => {
          const { id, name, type } =ship
          return (
            <tr key={id}>
              <td>{id}</td>
              <td>{name}</td>
              <td>{mapTypeName(type)}</td>
              <td></td>
            </tr>
          )
        })}
        </tbody>
      </Table>
    )
  }

  if (loading)
    return <Row style={{ marginTop: 20 }}><Preloader /></Row>;

  if (error)
    return <p>Falha ao carregar poderes especiais do servidor</p>;

  if (ships.length === 0)
    return <p>Não há nenhum navio cadastrado</p>;

  return (
    <div>
      {renderTable()}
    </div>
  )
}