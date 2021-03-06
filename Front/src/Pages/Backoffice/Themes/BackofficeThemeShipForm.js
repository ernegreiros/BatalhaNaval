import React, {useState} from "react";
import ApiClient from "../../../Repositories/ApiClient";
import {Button, Select, TextInput} from "react-materialize";
import ShipsTypes from "../../../Enums/ShipsTypes";
import PopUp from "../../../Components/PopUp/PopUp";

import './BackofficeThemeShipForm.css';

export function BackofficeThemeShipForm({ currentShip, onSaveSuccess, disabledTypes }) {
  const { OneField, TwoFields, ThreeFields, FourFields, FiveFields } = ShipsTypes;
  const options = [
    // esconder porque geralmente não tem navio de tamanho 1 { text: "1", value: OneField },
    { text: "2", value: TwoFields, disabled: disabledTypes.includes(TwoFields) },
    { text: "3", value: ThreeFields, disabled: disabledTypes.includes(ThreeFields) },
    { text: "4", value: FourFields, disabled: disabledTypes.includes(FourFields) },
    { text: "5", value: FiveFields, disabled: disabledTypes.includes(FiveFields) },
  ];
  const serializeShipToForm = ship => ({ ...ship });
  const [ship, setShip] = useState(currentShip.id ? serializeShipToForm(currentShip) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  function openCloudinaryWidget(e) {
    e.preventDefault();
    window.cloudinary.openUploadWidget({
      cloud_name: "venturi-x", upload_preset: "jmjebxux", sources:['local'], cropping: true
    }, function(error, result) {
      if (!error)
        return setShip({ ...ship, imagePath: result[0].url });

      if (error.message !== "User closed widget")
        return PopUp.showPopUp('error', 'Falha ao fazer upload da imagem!');
    });
  }

  function handleSubmit() {
    const requestData = {
      ...ship,
      themeId: currentShip.themeId,
      type: Number(ship.type)
    };

    setError(false);
    setSaving(true);

    currentShip.id
      ? ApiClient.UpdateShip(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
      : ApiClient.CreateShip(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
  }

  function onSuccess() {
    PopUp.showPopUp('sucess', 'Navio salvo com sucesso!');
    onSaveSuccess();
  }

  const isNameValid = ship.name !== undefined;
  const isValid = isNameValid;

  return (
    <div className="container">
      <br />
      <form>
        <TextInput
          label="Nome"
          defaultValue={ship.name}
          onChange={({ target }) =>
            setShip({ ...ship, name: target.value })} />

        <Select
          label="Tamanho do Navio"
          defaultValue={ship.type || ""}
          onChange={({ target }) => setShip({ ...ship, type: target.value })}
          multiple={false}
          options={{
            dropdownOptions: {
              alignment: 'left',
              autoTrigger: true,
              closeOnClick: true,
              constrainWidth: true,
              coverTrigger: true,
              hover: false,
              inDuration: 150,
              onCloseEnd: null,
              onCloseStart: null,
              onOpenEnd: null,
              onOpenStart: null,
              outDuration: 250
            }
          }}
        >
          <option disabled value="">Quantidade de campos</option>
          {options.map(({ value, text, disabled }) =>
            <option key={value} disabled={disabled} value={value}>{text}</option>)}
        </Select>

        {ship.imagePath
          ? <img src={ship.imagePath} alt="imagem de capa do tema"/>
          : <p>Navio sem imagem de capa</p>}

        <Button style={{ marginTop: 10 }} onClick={e => openCloudinaryWidget(e)}>Upload da imagem do navio</Button>

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