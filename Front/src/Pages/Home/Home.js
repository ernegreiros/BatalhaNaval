import React, { Fragment } from 'react';
import 'materialize-css/dist/css/materialize.min.css';
import InitialForm from '../../Components/InitialForm/InitialForm';
import NavBar from '../../Components/NavBar/NavBar';

// window.code = "";
// window.connection = new signalR.HubConnectionBuilder().withUrl("http:/localhost:5000/api/websocketHandler").build();

// async function init() {
//   await window.connection.start();
//
//   window.connection.invoke("RegisterCode", connection.connectionId).catch(function (err) {
//     return console.log(err.toString());
//   });
//
//   window.connection.on('CodeRegistered', function (code) {
//     window.code = code;
//   });
//
//   window.connection.on('Connected', function () {
//
//   });
// }

const Home = () => {
  
  // init()

  return (
    <Fragment>
      <NavBar />
      <div className="container">
        <br />
        <InitialForm />
      </div>
    </Fragment>
  );
}

export default Home;
