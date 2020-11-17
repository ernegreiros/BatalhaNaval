import React from 'react';
import ReactDOM from 'react-dom';
import  { Redirect,BrowserRouter, Switch, Route } from 'react-router-dom';

import * as serviceWorker from './serviceWorker';

import './index.css';

import Home from './Pages/Home/Home';
import BattleField from './Pages/BattleField/BattleField';
import BackofficeLogin from "./Pages/Backoffice/Login/BackofficeLogin";
import BackofficeHome from "./Pages/Backoffice/Home/BackofficeHome";
import SignUp from "./Pages/Home/SignUp";
import Login from "./Pages/Home/Login";
import UserService from "./Services/UserService";
import PopUp from "./Components/PopUp/PopUp";

const PrivateRoute = ({ component: Component, ...rest }) =>
  <Route
    {...rest}
    render={(props) => {
      if (!UserService().getToken()) {
        PopUp.showPopUp('error', 'Favor fazer o login para acessar o jogo');
        return <Redirect to='/' />
      }

      return <Component {...props} />
    }} />

ReactDOM.render(
  <BrowserRouter>
    <Switch>
      <Route path='/' exact={true} component={Login} />
      <PrivateRoute path='/Home' exact={true} component={Home} />
      <Route path='/signup' exact={true} component={SignUp} />
      <Route path='/battlefield' component={BattleField}/>
      <Route path='/backoffice-login' component={BackofficeLogin} />
      <Route path='/backoffice' component={BackofficeHome} />
    </Switch>
  </BrowserRouter>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
