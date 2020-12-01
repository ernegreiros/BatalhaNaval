import React, { Component, Fragment } from 'react'
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
      localStorage.setItem('player-ships', null);
      localStorage.setItem('opponent-ships', null);
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
      <Fragment>
        <div className="row" style={{ margin: "0 0 0 10%" }}>
          <form className="col s12">
            <br />
            <div className="row">
              <div className="col l10">
                <div className="input-field">
                  <input
                    placeholder="Código Segundo Jogador"
                    name="secondPlayerCode"
                    type="tel"
                    value={formValues.secondPlayerCode}
                    onChange={this.onChange} />
                </div>
              </div>
              <div className="col l2">
                <button onClick={this.onSubmit} className="darkbg btn right" style={{ height: "2.5em", marginTop: "26%", marginRight:"50%" }}>
                  <i className="material-icons ">play_circle_outline</i>
                </button>
              </div>
            </div>
          </form>
        </div>
      </Fragment>
    );
  }

}

export default InitialForm;