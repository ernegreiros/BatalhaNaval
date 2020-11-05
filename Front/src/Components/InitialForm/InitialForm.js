import React, { Component } from 'react'
import LinkWrapper from '../../Utils/LinkWrapper';
import Modal from '../Modal/Modal';
import PopUp from '../PopUp/PopUp';
import WebSocketHandler from '../WebSocketHandler/WebSocketHandler';

class InitialForm extends Component {

  constructor(props) {
    super(props);

    this.InitialState = {
      secondPlayerCode: ''
    }

    this.state = this.InitialState;
  }

  onChange = (event) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  }

  onSubmit = (event) => {
    event.preventDefault();
    const player = this.props.player;
    const formValues = { ...this.state };

    if (formValues.secondPlayerCode === '') {
      PopUp.showPopUp('error', 'Digite o código do segundo jogador!');
      return;
    }

    WebSocketHandler.AskForConnection(formValues.secondPlayerCode, player.code);
  }

  render() {
    const formValues = { ...this.state };
    const player = this.props.player;

    if (player.login !== "") {
      console.log(player.login);
      WebSocketHandler(player.login);
    }

    return (
      <div className="row" >
        <form className="col s12">
          <div className="row">
            <h3 className="center">Seja bem-vindo, <b>{player.name}</b>!</h3>
            <h4 className="center"><b>Seu Código: {player.code}</b></h4>
          </div>
          <br />
          <div className="row">
            <div className="input-field">
              <i className="material-icons prefix">contacts</i>
              <input
                placeholder="Código Segundo Jogador"
                name="secondPlayerCode"
                type="tel"
                value={formValues.secondPlayerCode}
                onChange={this.onChange} />
            </div>
          </div>
          <div className="row">
            <button onClick={this.onSubmit} className="darkbg btn-large right">
              <i className="material-icons right">play_circle_outline</i>
              Jogar
            </button>
          </div>
        </form>
      </div>
    );
  }

}

export default InitialForm;