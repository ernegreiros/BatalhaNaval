import React, {Fragment, useEffect, useState} from 'react';
import 'materialize-css/dist/css/materialize.min.css';
import InitialForm from '../../Components/InitialForm/InitialForm';
import NavBar from '../../Components/NavBar/NavBar';
import ApiClient from "../../Repositories/ApiClient";
import PopUp from "../../Components/PopUp/PopUp";

// window.code = "";
// window.connection = new signalR.HubConnectionBuilder().withUrl("http:/localhost:5000/api/websocketHandler").build();


const Home = () => {
  const [player, setPlayer] = useState({ name: "", code: "" });

  useEffect(() => {
    init();
  }, [])

  function init() {
    ApiClient.GetPlayer()
      .then(({ player }) => setPlayer({ name: player.name, code: player.code }))
      .catch(() => PopUp.showPopUp('error', 'Falha ao obter dados do jogador'))

    // await window.connection.start();
    //
    // window.connection.invoke("RegisterCode", connection.connectionId).catch(function (err) {
    //   return console.log(err.toString());
    // });
    //
    // window.connection.on('CodeRegistered', function (code) {
    //   window.code = code;
    // });
    //
    // window.connection.on('Connected', function () {
    //
    // });
  }

  return (
    <Fragment>
      <NavBar />
      <div className="container">
        <br />
        <InitialForm player={player} />
      </div>
    </Fragment>
  );
}

export default Home;
