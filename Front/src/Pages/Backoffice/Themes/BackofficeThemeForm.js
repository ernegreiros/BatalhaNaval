import React, { useState } from "react";
import { TextInput, Button } from "react-materialize";

import './BackofficeThemeForm.css';

import ApiClient from "../../../Repositories/ApiClient";
import {toBase64} from "../../../Utils/fileHelpers";

export function BackofficeThemeForm({ currentTheme = {}, onSaveSuccess }) {
  const serializeThemeToForm = theme => ({ ...theme });
  const [theme, setTheme] = useState(currentTheme.id ? serializeThemeToForm(currentTheme) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  async function handleImageChange(event) {
    const { files } = event.target

    toBase64(files[0])
      .then(base64 => setTheme({ ...theme, imagePath: base64 }))
      .catch(() => alert('Falha ao atualizar imagem'))
  }

  function handleSubmit() {
    const requestData = { ...theme };

    setError(false);
    setSaving(true);

    currentTheme.id
      ? ApiClient.UpdateTheme(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
      : ApiClient.CreateTheme(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
  }

  function onSuccess() {
    alert('Tema salvo com sucesso!')
    onSaveSuccess();
  }

  const isNameValid = theme.name !== undefined;
  const isDescriptionValid = theme.description !== undefined;
  const isValid = isNameValid && isDescriptionValid;

  return (
    <div className="container">
      <br />
      <form>
        {theme.imagePath && <img src={theme.imagePath} alt="imagem de capa do tema"/>}
        <TextInput
          label="Nome"
          defaultValue={theme.name}
          onChange={({ target }) =>
            setTheme({ ...theme, name: target.value })} />
        <TextInput
          label="Descrição"
          defaultValue={theme.description}
          onChange={({ target }) =>
            setTheme({ ...theme, description: target.value })} />

        <TextInput
          label="Selecione imagem de capa do tema"
          type="file"
          onChange={handleImageChange} />

        <Button onClick={() => handleSubmit()} disabled={saving || !isValid}>
          {saving ? "Salvando..." : "Salvar"}
        </Button>
        {error && <section>
          <small>Falha ao salvar no servidor</small>
        </section>}
      </form>
    </div>

  )
}