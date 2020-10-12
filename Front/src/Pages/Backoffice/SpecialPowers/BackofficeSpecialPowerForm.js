import React, { useState } from "react";
import { TextInput, Select, Button } from "react-materialize";

import SpecialPowerTypes from "../../../Enums/SpecialPowerTypes";
import ApiClient from "../../../Repositories/ApiClient";

export function BackofficeSpecialPowerForm({ currentSpecialPower = {}, onSaveSuccess }) {
  const { Attack, Defense, Preview } = SpecialPowerTypes;
  const options = [
    { text: "Ataque", value: Attack },
    { text: "Defesa", value: Defense },
    { text: "Previsão", value: Preview },
  ];
  const serializeSpecialPowerToForm = specialPower => ({
    ...specialPower,
    type: specialPower.type.toString(),
    quantifier: specialPower.quantifier.toString(),
    cost: specialPower.cost.toString(),
    compensation: specialPower.compensation.toString()
  });
  const [specialPower, setSpecialPower] = useState(currentSpecialPower.id ? serializeSpecialPowerToForm(currentSpecialPower) : {})
  const [error, setError] = useState(false)
  const [saving, setSaving] = useState(false)

  function handleSubmit() {
    const { type, quantifier, cost, compensation } = specialPower;
    const requestData = {
      ...specialPower,
      type: Number(type),
      quantifier: Number(quantifier),
      cost: Number(cost),
      compensation: Number(compensation)
    };

    setError(false);
    setSaving(true);

    currentSpecialPower.id
      ? ApiClient.UpdateSpecialPower(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
      : ApiClient.CreateSpecialPower(requestData)
        .then(() => onSuccess())
        .catch(() => {
          setError(true)
          setSaving(false)
        })
  }

  function onSuccess() {
    alert('Poder especial salvo com sucesso!')
    onSaveSuccess();
  }

  const isNameValid = specialPower.name !== undefined;
  const isQuantifierValid = !isNaN(specialPower.quantifier);
  const isTypeValid = !isNaN(specialPower.type);
  const isCostValid = !isNaN(specialPower.cost);
  const isCompensationValid = !isNaN(specialPower.compensation);
  const isValid = isNameValid && isQuantifierValid && isTypeValid && isCostValid && isCompensationValid;

  return (
    <div className="container">
      <br />
      <form>
        <TextInput
          label="Nome"
          defaultValue={specialPower.name}
          onChange={({ target }) =>
            setSpecialPower({ ...specialPower, name: target.value })} />
        <TextInput
          type="number"
          label="Quantidade"
          defaultValue={specialPower.quantifier}
          onChange={({ target }) =>
            setSpecialPower({ ...specialPower, quantifier: target.value })} />
        <Select
          label="Tipo do poder"
          defaultValue={specialPower.type || ""}
          onChange={({ target }) =>
            setSpecialPower({ ...specialPower, type: target.value })}
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
          <option disabled value="">Escolha o tipo</option>
          {options.map(({ value, text }) =>
            <option key={value} value={value}>{text}</option>)}
        </Select>
        <TextInput
          type="number"
          defaultValue={specialPower.cost}
          label="Valor"
          onChange={({ target }) =>
            setSpecialPower({ ...specialPower, cost: target.value })} />
        <TextInput
          type="number"
          defaultValue={specialPower.compensation}
          label="Recompensa"
          onChange={({ target }) =>
            setSpecialPower({ ...specialPower, compensation: target.value })} />

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