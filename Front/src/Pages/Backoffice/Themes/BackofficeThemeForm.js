import React, { useState } from "react";
import { TextInput, Button } from "react-materialize";

import './BackofficeThemeForm.css';

import ApiClient from "../../../Repositories/ApiClient";

export function BackofficeThemeForm({ currentTheme = {}, onSaveSuccess }) {
  const serializeThemeToForm = theme => ({ ...theme });
  const [theme, setTheme] = useState(currentTheme.id ? serializeThemeToForm(currentTheme) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  function openCloudinaryWidget(e) {
    e.preventDefault();
    window.cloudinary.openUploadWidget({
      cloud_name: "venturi-x", upload_preset: "jmjebxux", sources:['local'], cropping: true
    }, function(error, [result]) {
      if (error)
        return alert("Falha ao fazer upload da imagem");
      console.log(result)
      setTheme({ ...theme, imagePath: result.url });
    });
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

        {theme.imagePath
          ? <img src={theme.imagePath} alt="imagem de capa do tema"/>
          : <p>Tema sem imagem de capa</p>}

        <Button style={{ marginTop: 10 }} onClick={e => openCloudinaryWidget(e)}>Upload da imagem de capa do tema</Button>

        <div style={{ marginTop: 30 }}>
          <Button onClick={() => handleSubmit()} disabled={saving || !isValid}>
            {saving ? "Salvando..." : "Salvar"}
          </Button>
          {error && <section>
            <small>Falha ao salvar no servidor</small>
          </section>}
        </div>
      </form>
    </div>

  )
}