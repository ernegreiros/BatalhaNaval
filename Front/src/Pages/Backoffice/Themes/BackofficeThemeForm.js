import React, { useState } from "react";
import { TextInput, Button } from "react-materialize";

import './BackofficeThemeForm.css';

import ApiClient from "../../../Repositories/ApiClient";
import {BackofficeThemeShips} from "./BackofficeThemeShips";
import PopUp from "../../../Components/PopUp/PopUp";

export function BackofficeThemeForm({ currentTheme = {}, onSaveSuccess }) {
  const serializeThemeToForm = theme => ({ ...theme });
  const [theme, setTheme] = useState(currentTheme.id ? serializeThemeToForm(currentTheme) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  function openCloudinaryWidget(e) {
    e.preventDefault();
    window.cloudinary.openUploadWidget({
      cloud_name: "venturi-x", upload_preset: "jmjebxux", sources:['local'], cropping: true
    }, function(error, result) {
      if (!error)
        return setTheme({ ...theme, imagePath: result[0].url });

      if (error.message !== "User closed widget")
        return PopUp.showPopUp('error', 'Falha ao fazer upload da imagem!');
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
    PopUp.showPopUp('success', 'Tema salvo com sucesso!');
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
        <p>A Imagem de capa do tema, aparece como uma imagem de fundo para o jogador!</p>

        {theme.id && (
          <>
            <br />
            <br />
            <h5>Navios - {theme.name}</h5>
            <br />
            <BackofficeThemeShips themeId={theme.id} />
          </>
        )}

        <div style={{ marginTop: 30 }}>
          <Button onClick={() => handleSubmit()} disabled={saving || !isValid}>
            {saving ? "Salvando..." : "Salvar"}
          </Button>
          {error && <section>
            <small>Falha ao salvar no servidor</small>
          </section>}
        </div>
      </form>
      <br />
      <p>Após o cadastro do tema é possivel adicionar os navios!</p>
    </div>
  )
}