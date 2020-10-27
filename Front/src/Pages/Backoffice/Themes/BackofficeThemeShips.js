import React, { useEffect, useState } from "react";
import { Button, Modal, Preloader, Row, Table } from "react-materialize";

import ApiClient from "../../../Repositories/ApiClient";
import ShipsTypes from "../../../Enums/ShipsTypes";
import { BackofficeThemeShipForm } from "./BackofficeThemeShipForm";

export function BackofficeThemeShips({ themeId }) {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [ships, setShips] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingShip, setEditingShip] = useState({});
  let ShipsMessage = '';

  useEffect(() => getShips(), [])

  function getShips() {
    setError(false);
    setLoading(true);
    ApiClient.GetThemeShips(themeId)
      .then(({ ship }) => setShips(ship))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  function handleDelete(ship) {
    ApiClient.DeleteShip(ship.id)
      .then(() => {
        alert('Navio excluído');
        getShips();
      })
      .catch(() => alert('Falha ao excluir o navio'))
  }

  function handleModalClose() {
    setIsEditing(false)
    setEditingShip({})
  }

  function handleEdit(ship) {
    setIsEditing(true)
    setEditingShip(ship)
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
            <th>Tamanho</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {ships.map(ship => {
            const { id, name, type } = ship
            return (
              <tr key={id}>
                <td>{id}</td>
                <td>{name}</td>
                <td>{mapTypeName(type)}</td>
                <td>
                  <Button
                    onClick={(e) => {
                      e.preventDefault();
                      handleEdit(ship);
                    }}
                  >
                    Editar
                </Button>
                </td>
                <td>
                  <Button
                    style={{backgroundColor:"red"}}
                    onClick={(e) => {
                      e.preventDefault();
                      handleDelete(ship);
                    }}
                  >
                    Excluir
                </Button>
                </td>
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
    ShipsMessage = <p>Não há nenhum navio cadastrado</p>;

  return (
    <div>
      <p>{ShipsMessage}</p>
      {(!error && !loading) && <Modal
        style={{ width: 700 }}
        fixedFooter={false}
        actions={[]}
        header="Edição - Navio"
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
        trigger={<div><Button node="button" className="right">Cadastrar novo navio</Button><br /><br /></div>}
      >
        {isEditing && <BackofficeThemeShipForm
          currentShip={{ ...editingShip, themeId }}
          onSaveSuccess={() => {
            setIsEditing(false)
            getShips()
          }} />}
      </Modal>}
      {renderTable()}
    </div>
  )
}