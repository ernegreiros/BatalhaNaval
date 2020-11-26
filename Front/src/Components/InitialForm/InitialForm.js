import React, { Component } from 'react'
import HelperModal from '../../Utils/HelperModal';
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

  async componentDidUpdate() {
    const player = this.props.player;

    if (player.login !== "") {
      console.log(player.login);

      this.hubConnection = await WebSocketHandler(player.login);

      this.addConnectionHandlers();
    }
  }

  addConnectionHandlers() {
    
    this.removeHandlers();

    this.hubConnection.on("AskingForConnection", function (player, myCode) {
      HelperModal.ShowWantToConnectModal(player, myCode);
    });

    this.hubConnection.on("Connected", function (matchId, player, partnerPlayer) {
      localStorage.setItem('match', JSON.stringify({ "matchId": matchId, "adversary": partnerPlayer, "player": player }));
      localStorage.setItem('battle-field-theme', null)
      localStorage.setItem('StartMatch', false);
      PopUp.showPopUp('success', 'Conectado');
      window.location.href = '/battlefield';
    });

    this.hubConnection.on("ConnectionRefused", function () {
      PopUp.showPopUp('error', 'Pedido de jogo recusado');
    });
  }

  removeHandlers() {
    this.hubConnection.off("AskingForConnection");
    this.hubConnection.off("Connected");
    this.hubConnection.off("ConnectionRefused");
  }

  render() {
    const formValues = { ...this.state };
    const player = this.props.player;

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