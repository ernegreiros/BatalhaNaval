import React, { useEffect, useState } from "react";
import { Table, Preloader, Row, Modal, Button } from 'react-materialize';
import PopUp from "../../../Components/PopUp/PopUp";

import ApiClient from "../../../Repositories/ApiClient";
import { BackofficeThemeForm } from "./BackofficeThemeForm";

export default function BackofficeThemes() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editingTheme, setEditingTheme] = useState({});
  const [themes, setThemes] = useState([]);

  useEffect(() => getThemes(), []);

  function getThemes() {
    setError(false);
    setLoading(true);
    ApiClient.GetThemes()
      .then(({ themes }) => setThemes(themes))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  function handleModalClose() {
    setIsEditing(false)
    setEditingTheme({})
  }

  function handleEdit(theme) {
    setIsEditing(true)
    setEditingTheme(theme)
  }

  function handleDelete(theme) {
    if (window.confirm("Você realmente deseja deletar? todos os navios desse tema serão apagados!")) {
      ApiClient.DeleteTheme(theme.id)
        .then(() => {
          PopUp.showPopUp('success', 'Removido com sucesso!');
          getThemes();
        })
        .catch(() => PopUp.showPopUp('error', 'Falha ao excluir o tema'))
    }
  }

  function renderTable() {
    return (
      <Table style={{ margin: 20 }}>
        <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Descrição</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {themes.map(theme => {
            const { id, name, description } = theme
            return (
              <tr key={id}>
                <td>{id}</td>
                <td>{name}</td>
                <td>{description}</td>
                <td>
                  <Button onClick={() => handleEdit(theme)}>Editar</Button>
                </td>
                <td>
                  <Button style={{ backgroundColor: "red" }} onClick={() => handleDelete(theme)}>Excluir</Button>
                </td>
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
      <h6>Após o cadastro do tema é possivel adicionar os navios!</h6>
      <br />      

      {(!error && !loading) && <Modal
        style={{ width: 700 }}
        fixedFooter={false}
        actions={[]}
        header="Edição - Tema"
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
        trigger={<div><Button node="button" className="right">Cadastrar novo tema</Button><br /><br /></div>}
      >
        {isEditing && <BackofficeThemeForm
          currentTheme={editingTheme}
          onSaveSuccess={() => {
            setIsEditing(false)
            getThemes()
          }} />}
      </Modal>}

      {error
        ? <p>Falha ao carregar Temas do servidor</p>
        : loading
          ? <Row style={{ marginTop: 20 }}><Preloader /></Row>
          : themes.length === 0
            ? <p>Não há nenhum tema cadastrado</p>
            : renderTable()}
    </div>
  )
}