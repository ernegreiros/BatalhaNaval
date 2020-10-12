import React, {useEffect, useState} from "react";
import {Table, Modal, Button, Preloader, Row} from 'react-materialize';

import ApiClient from "../../../Repositories/ApiClient";
import SpecialPowerTypes from "../../../Enums/SpecialPowerTypes";
import {formatToMoney} from "../../../Utils/MoneyHelper";
import {BackofficeSpecialPowerForm} from "./BackofficeSpecialPowerForm";

export default function BackofficeSpecialPowers() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editingSpecialPower, setEditingSpecialPower] = useState({});
  const [specialPowers, setSpecialPowers] = useState([]);

  useEffect(() => getSpecialPowers(), []);

  function getSpecialPowers() {
    setError(false);
    setLoading(true);
    ApiClient.GetSpecialPowers()
      .then(data => setSpecialPowers(data))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  function mapSpecialPowerTypeName(type) {
    const { Attack, Defense, Preview } = SpecialPowerTypes;
    const names = {
      [Attack]: "Ataque",
      [Defense]: "Defesa",
      [Preview]: "Previsão",
    }
    return names[type];
  }

  function handleModalClose() {
    setIsEditing(false)
    setEditingSpecialPower({})
  }

  function handleEdit(specialPower) {
    setIsEditing(true)
    setEditingSpecialPower(specialPower)
  }

  function renderTable() {
    return (
      <Table style={{ margin: 20 }}>
        <thead>
        <tr>
          <th>#</th>
          <th>Nome</th>
          <th>Tipo</th>
          <th>Quantidade</th>
          <th>Preço</th>
          <th>Compensação</th>
          <th>Ações</th>
        </tr>
        </thead>
        <tbody>
          {specialPowers.map((specialPower, index) => {
            const { id, name, quantifier, type, cost, compensation} = specialPower;
            return (
              <tr key={index}>
                <td>{id}</td>
                <td>{name}</td>
                <td>{mapSpecialPowerTypeName(type)}</td>
                <td>{quantifier}</td>
                <td>{formatToMoney(cost)}</td>
                <td>{formatToMoney(compensation)}</td>
                <td><Button onClick={() => handleEdit(specialPower)}>Editar</Button></td>
              </tr>
            )
          })}
        </tbody>
      </Table>
    )
  }

  return (
    <div>
      <h4>Cadastros - Poderes Especiais</h4>
      <br />

      {(!error && !loading) && <Modal
        fixedFooter={false}
        actions = {[]}
        header="Edição - Poder Especial"
        open={isEditing}
        options={{
          dismissible: true,
          endingTop: '10%',
          inDuration: 250,
          onCloseStart: null,
          onOpenEnd: () => setIsEditing(true),
          opacity: 0.5,
          outDuration: 250,
          preventScrolling: true,
          startingTop: '4%',
          onCloseEnd: () => handleModalClose()
        }}
        trigger={<div><Button node="button" className="right">Cadastrar novo poder</Button><br /><br /></div>}
      >
        
        {isEditing && <BackofficeSpecialPowerForm
          currentSpecialPower={editingSpecialPower}
          onSaveSuccess={() => {
            setIsEditing(false)
            getSpecialPowers()
          }} />}
      </Modal>}

      {error
        ? <p>Falha ao carregar poderes especiais do servidor</p>
        : loading
          ? <Row style={{ marginTop: 20 }}><Preloader /></Row>
          : specialPowers.length === 0
            ? <p>Não há nenhum poder especial cadastrado</p>
            : renderTable()
      }
    </div>
  )
}