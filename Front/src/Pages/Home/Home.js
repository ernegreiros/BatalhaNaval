import React, { Fragment, useEffect, useState } from 'react';
import M from "materialize-css";
import 'materialize-css/dist/css/materialize.min.css';
import InitialForm from '../../Components/InitialForm/InitialForm';
import NavBar from '../../Components/NavBar/NavBar';
import ApiClient from "../../Repositories/ApiClient";
import PopUp from "../../Components/PopUp/PopUp";
import Modal from '../../Components/Modal/Modal';
import UserService from "../../Services/UserService";
import ParallaxImage from '../../Components/ParallaxImage/ParallaxImage';
import ShipShootImage from '../../Images/ShipShoot2.jpg';

const Home = ({ history }) => {

  const [player, setPlayer] = useState({ name: "", code: "", login: "", money: "" });

  useEffect(() => {
    
    init();
    
    M.AutoInit();

  }, [])

  async function init() {
    await ApiClient.GetPlayer()
      .then(({ player }) => {
        setPlayer({ name: player.name, code: player.code, login: player.login, money: player.money })
        UserService().setPlayerData(player);
      })
      .catch((error) => {
        console.log(error);
        PopUp.showPopUp('error', 'Falha ao obter dados do jogador');
        history.push('/')
      });
  }

  return (
    <Fragment>
      <NavBar />

      <ParallaxImage imagePath={ShipShootImage} style={{ height: "15em", opacity: ".25" }} />

      <div className="row" style={{ marginLeft: "" }}>
        <h5 className="center">Seja bem-vindo, <b>{player.name}</b>!  Você tem <b>{player.money.toLocaleString('pt-br', { minimumFractionDigits: 2 })}</b> BN Points </h5>
        <h5 className="center" >Seu Código: <b>{player.code}</b></h5>
      </div>

      <br />
      <br />
      <br />

      <div className="row" style={{ margin: "-5% 0 0 5%" }}>
        <div className="col l2">

          <div className="row">
            <h5 className="left">Vamos Jogar!?</h5>
          </div>

          <div className="row" style={{ marginLeft: "" }}>
            <p>Para jogar, digite o código do seu amigo no campo ao lado, caso ele aceite o jogo irá começar!</p>
          </div>
        </div>

        <div className="col l10" style={{ marginTop: "5%" }}>
          <InitialForm player={player} />
        </div>

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