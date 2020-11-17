import React, { Component, Fragment } from "react";
import { Button, Carousel } from "react-materialize";

import './BattleField.css';

import { createPlayer, welcomeMessage } from "../../Utils/gameHelpers";
import BattleGrid from "../../Components/BattleGrid/BattleGrid";
import ShipGrid from "../../Components/ShipsGrid/ShipGrid";
import NavBar from "../../Components/NavBar/NavBar";
import ApiClient from "../../Repositories/ApiClient";
import PopUp from "../../Components/PopUp/PopUp";
import UserService from "../../Services/UserService";

const themeFromLocalStorage = localStorage.getItem('battle-field-theme');
// workaround para evitar rerender do carousel
let theme = themeFromLocalStorage ? JSON.parse(themeFromLocalStorage) : null;

class BattleField extends Component {
  constructor(props) {
    super(props);

    this.state = {
      activePlayer: "player",
      player: createPlayer(),
      themes: [],
      themeSelected: theme !== null,
      theme: theme !== null ? theme : null,
      loadingTheme: true,
      themeShips: [],
      settingThemeShip: null,
      allShipsSet: false,
      gameStarted: false,
      waitingAdversary: false,
      winner: null,
      gameOver: false,
      logs: [welcomeMessage]
    };

    this.updateGrids = this.updateGrids.bind(this);
    this.updateShips = this.updateShips.bind(this);
  }

  componentDidMount() {
    ApiClient.GetThemes()
      .then(({ themes }) => this.setState({ themes }))
      .catch(() => PopUp.showPopUp('error', 'Falha ao carregar temas'))
      .finally(() => this.setState({ loadingTheme: false }))

    if (theme !== null) this.getThemeShips()
  }

  getThemeShips = () => {
    ApiClient.GetThemeShips(theme.id)
      .then(({ ship }) => this.setState({ themeShips: ship, settingThemeShip: ship[0] }))
      .catch(() => PopUp.showPopUp('error', 'Falha ao carregar navios do tema'))
  }

  onCarouselCycle = element => {
    const { themes } = this.state;
    const index = element.getAttribute('data-index');
    const cycledTheme = themes[Number(index)];

    if (!theme || cycledTheme.id !== theme.id) theme = cycledTheme;
  }

  selectTheme = () => {
    this.setState({ themeSelected: true, theme }, () => {
      localStorage.setItem('battle-field-theme', JSON.stringify(theme))
      this.getThemeShips()
    })
  }

  updateGrids(player, grid, type, opponent) {
    const payload = {
      player,
      grid,
      type,
      opponent
    }
    this.gridReducer("UPDATE", payload);
    if (opponent && opponent.sunkenShips === 5) {
      this.gridReducer("GAME_OVER", payload);
    } else if (opponent) {
      this.gridReducer("HIT", payload);
    }
  }

  gridReducer(action, { player, grid, type, opponent }) {
    const other = "player2";
    if (action === "UPDATE") {
      const updatedPlayer = {
        ...this.state[player],
        [this.state[player][type]]: grid
      };
      this.setState({
        [player]: updatedPlayer
      });
    }
    if (action === "GAME_OVER") {
      this.setState({
        gameOver: true,
        activePlayer: null,
        winner: player
      });
    }
    if (action === "HIT") {
      this.setState({
        [other]: opponent,
        activePlayer: other
      });
    }
  }

  renderBattleGrid(player) {
    const opponent = "player2";
    const { activePlayer } = this.state;
    return (
      <BattleGrid
        player={player}
        grid={this.state[player].movesGrid}
        opponent={this.state[opponent]}
        updateGrids={this.updateGrids}
        activePlayer={activePlayer}
        shipsSet={this.state[player].shipsSet}
      />
    );
  }

  renderShipGrid(player) {
    const { activePlayer, gameOver, themeShips } = this.state;

    return (
      <ShipGrid
        player={player}
        grid={this.state[player].shipsGrid}
        ships={this.state[player].ships}
        currentShip={this.state[player].currentShip}
        themeShips={themeShips}
        updateGrids={this.updateGrids}
        updateShips={this.updateShips}
        shipsSet={this.state[player].shipsSet}
        activePlayer={activePlayer}
        gameOver={gameOver}
      />
    );
  }

  renderShips = () => {
    const { themeShips, theme, player } = this.state;
    const settingThemeShipSize = player.ships.find(ship => ship.positions.length === 0)?.size ?? 5;

    return (
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start', gap: 20, marginLeft: 30 }}>
        <h4>Navios - {theme.name}</h4>
        {themeShips.map(themeShip =>
          <img className={themeShip.type === settingThemeShipSize ? "theme-ship active" : ""} key={themeShip.id} src={themeShip.imagePath} />)}
      </div>
    )
  }

  updateShips(player, updatedShips) {
    const { ships, currentShip } = this.state[player];
    const payload = {
      updatedShips,
      player
    }
    if (currentShip + 1 === ships.length ) {
      PopUp.showPopUp("error", "Colocou todos os navios")
      this.shipReducer("START_GAME", payload); 
    } else {
      this.shipReducer("SET_SHIP", payload)
    }
  }

  shipReducer(action, { updatedShips, player }) {
    const { currentShip, ships } = this.state[player];
    const positionedShips = ships.filter(ship => ship.positions.length > 0);

    if (action === "SET_PLAYER_ONE") {
      this.setState({
        player1: {
          ...this.state.player1,
          ships: updatedShips,
          shipsSet: true
        },
        activePlayer: "player2"
      });
    }
    if (action === "SET_PLAYER_TWO") {
      this.setState({
        player2: {
          ...this.state.player2,
          ships: updatedShips,
          shipsSet: true
        },
        allShipsSet: true,
        gameStarting: true
      });
    }
    if (action === "START_GAME") {
      setTimeout(() => {
        this.setState({
          activePlayer: "player1",
          gameStarting: false
        });
      }, 3000);
    }
    if (action === "SET_SHIP") {
      const updatedPlayer = {
        ...this.state[player],
        ships: updatedShips,
        currentShip: positionedShips.length < 5 ? currentShip + 1 : 4
      };
      this.setState({
        [player]: updatedPlayer
      });
    }
  }

  startBattleShip = () => {
    const { ships } = this.state.player;
    const matchId = localStorage.getItem('match-id');
    const shipsPositions = ships
      .map(ship => ship.positions)
      .reduce((current, next) => [...current, ...next], []);
    const positions = shipsPositions.map(position => ({
      Player: UserService().getPlayerData().id,
      MatchID: Number(matchId),
      PosX: position.col,
      PosY: position.row
    }));

    ApiClient.RegisterPositions(positions)
      .then(() => this.setState({ waitingAdversary: true }))
      .catch(() => PopUp.showPopUp('error', 'Falha ao enviar posições para o servidor'))
  }

  render() {
    const { themes, loadingTheme, themeSelected, gameStarted, player, waitingAdversary } = this.state;
    const positionedAllShips = player.ships.every(ship => ship.positions.length > 0);

    return (
      <Fragment>
        <NavBar />
        {!themeSelected && (
          <div>
            <h1>Escolha o Tema</h1>
            {!loadingTheme && (
              <div>
                <Carousel
                  className="battle-field-themes-carousel"
                  images={themes.map(({ imagePath }) => imagePath)}
                  options={{
                    dist: -100,
                    fullWidth: false,
                    indicators: false,
                    noWrap: false,
                    numVisible: 5,
                    onCycleTo: this.onCarouselCycle,
                    padding: 0,
                    shift: 0,
                  }}
                >
                  {themes.map((theme, index) => (
                    <div key={index} data-index={index} style={{ textAlign: 'center' }}>
                      <img src={theme.imagePath} />
                      <h4>{theme.name}</h4>
                      <small>{theme.description}</small>
                    </div>
                  ))}
                </Carousel>
                <br />
                <div className="select-theme-container">
                  <Button onClick={() => this.selectTheme()}>Continuar</Button>
                </div>
              </div>
            )}
          </div>
        )}
        {themeSelected && (
          <div className="game">
            <img className="battlefield-background" src={theme.imagePath} />
            <div className="row">
              <div className="col l6 s12" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                {this.renderShipGrid("player")}
                <Button onClick={() => this.startBattleShip()} disabled={!positionedAllShips || waitingAdversary}>
                  {waitingAdversary ? 'Aguardando adversário' : 'INICIAR'}
                </Button>
              </div>
              <div className="col l6 s12">
                {gameStarted ? this.renderBattleGrid("player") : this.renderShips()}
              </div>
            </div>
          </div>
        )}
      </Fragment>
    );
  }
}

export default BattleField