import React, {useEffect, useState} from "react";
import {Spinner, Table} from "react-bootstrap";

import ApiClient from "../../../Repositories/ApiClient";
import SpecialPowerTypes from "../../../Enums/SpecialPowerTypes";
import {formatToMoney} from "../../../Utils/MoneyHelper";

export default function BackofficeSpecialPowers() {
  const [loading, setLoading] = useState(false)
  const [specialPowers, setSpecialPowers] = useState([])

  useEffect(() => {
    setLoading(true);
    ApiClient.GetSpecialPowers()
      .then(data => setSpecialPowers(data))
      .finally(() => setLoading(false))
  }, [])

  function mapSpecialPowerTypeName(type) {
    const { Attack, Defense, Preview } = SpecialPowerTypes;
    const names = {
      [Attack]: "Ataque",
      [Defense]: "Defesa",
      [Preview]: "Previsão",
    }
    return names[type];
  }

  return (
    <div style={{ padding: 20 }}>
      <h1>Cadastros - Super Poderes</h1>

      {loading
        ? <Spinner animation="border" />
        : <Table striped bordered responsive hover style={{ margin: 20 }}>
          <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Tipo</th>
            <th>Preço</th>
            <th>Compensação</th>
          </tr>
          </thead>
          <tbody>
          {specialPowers.map(({ id, name, type, cost, compensation}, index) => (
            <tr>
              <td>{id}</td>
              <td>{name}</td>
              <td>{mapSpecialPowerTypeName(type)}</td>
              <td>{formatToMoney(cost)}</td>
              <td>{formatToMoney(compensation)}</td>
            </tr>
          ))}
          </tbody>
        </Table>
      }
    </div>
  )
}