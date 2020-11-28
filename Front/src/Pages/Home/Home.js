import React, { Fragment, useEffect, useState } from 'react';
import 'materialize-css/dist/css/materialize.min.css';
import InitialForm from '../../Components/InitialForm/InitialForm';
import NavBar from '../../Components/NavBar/NavBar';
import ApiClient from "../../Repositories/ApiClient";
import PopUp from "../../Components/PopUp/PopUp";
import Modal from '../../Components/Modal/Modal';

const Home = ({ history }) => {
  const [player, setPlayer] = useState({ name: "", code: "", login: "" });

  useEffect(() => {
    init();
  }, [])

  async function init() {
    await ApiClient.GetPlayer()
      .then(({ player }) => setPlayer({ name: player.name, code: player.code, login: player.login }))
      .catch(
        () => {
          PopUp.showPopUp('error', 'Falha ao obter dados do jogador');
          history.push('/')
        });
  }

  return (
    <Fragment>
      <NavBar />
      <div className="container">
        <br />
        <InitialForm player={player} />
      </div>
      <Modal
        ModalId="HelperModal"
        ModalHeader=""
        ModalBody=""
      />
    </Fragment>
  );
}

export default Home;