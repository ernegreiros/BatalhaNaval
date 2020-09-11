import React, { Fragment } from 'react';
import 'materialize-css/dist/css/materialize.min.css';
import InitialForm from '../../Components/InitialForm/InitialForm';
import NavBar from '../../Components/NavBar/NavBar';

const Home = () => {
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
