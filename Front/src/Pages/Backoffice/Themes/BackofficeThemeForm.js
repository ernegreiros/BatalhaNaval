import React, { useState } from "react";
import { TextInput, Button } from "react-materialize";

import ApiClient from "../../../Repositories/ApiClient";

export function BackofficeThemeForm({ currentTheme = {}, onSaveSuccess }) {
  const serializeThemeToForm = theme => ({
    ...theme,
  });
  const [theme, setTheme] = useState(currentTheme.id ? serializeThemeToForm(currentTheme) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  function handleSubmit() {
    const requestData = {
      ...theme,
      imagePath: 'teste image path'
    };

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