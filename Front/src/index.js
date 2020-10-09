import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';

import * as serviceWorker from './serviceWorker';

import './index.css';

import Home from './Pages/Home/Home';
import BattleField from './Pages/BattleField/BattleField';
import BackofficeLogin from "./Pages/Backoffice/Login/BackofficeLogin";
import BackofficeHome from "./Pages/Backoffice/Home/BackofficeHome";

ReactDOM.render(
  <BrowserRouter>
    <Switch>
      <Route path='/' exact={true} component={Home} />
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
