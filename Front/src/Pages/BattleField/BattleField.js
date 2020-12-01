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
import WebSocketHandler from "../../Components/WebSocketHandler/WebSocketHandler";
import { MatchStatus } from "../../Enums/MatchStatus";
import LinkWrapper from "../../Utils/LinkWrapper";

const themeFromLocalStorage = localStorage.getItem('battle-field-theme');
// workaround para evitar rerender do carousel
let theme = themeFromLocalStorage ? JSON.parse(themeFromLocalStorage) : null;

class BattleField extends Component {
  constructor(props) {
    super(props);

    const playerShips = localStorage.getItem('player-ships')
    const opponentShips = localStorage.getItem('opponent-ships')

    this.state = {
      activePlayer: "player",
      player: createPlayer(playerShips ? JSON.parse(playerShips) : null),
      player2: createPlayer(opponentShips ? JSON.parse(opponentShips) : null),
      loadingMatch: false,
      match: {},
      themes: [],
      themeSelected: theme !== null,
      theme: theme !== null ? theme : null,
      loadingTheme: true,
      themeShips: [],
      currentSpecialPower: null,
      settingThemeShip: null,
      allShipsSet: false,
      gameStarted: false,
      gameOver: false,
      waitingAdversary: false,
      winner: null,
      logs: [welcomeMessage]
    };

    this.updateGrids = this.updateGrids.bind(this);
    this.updateShips = this.updateShips.bind(this);
  }


  async componentDidMount() {
    const playersReady = JSON.parse(localStorage.getItem('StartMatch'));

    this.match = JSON.parse(localStorage.getItem('match'));

    this.hubConnection = await WebSocketHandler(this.match.player.login);

    this.addHandlersForBattle();

    this.getCurrentMatch();
    this.getThemes();
    if (theme !== null) this.getThemeShips();
    if (playersReady) {
      this.getPositions();
      this.getOpponentData();
    }
  }

  getThemes = () => {
    ApiClient.GetThemes()
      .then(({ themes }) => this.setState({ themes }))
      .catch(() => PopUp.showPopUp('error', 'Falha ao carregar temas'))
      .finally(() => this.setState({ loadingTheme: false }))
  }

  getCurrentMatch = () => {
    this.setState({ loadingMatch: true }, () => {
      ApiClient.GetCurrentMatch()
        .then((match = {}) => {
          const playerInfo = UserService().getPlayerData();
          const playerReady = playerInfo.id === match.player1 ? match.player1Ready : match.player2Ready;
          const allPlayersReady = match.status === MatchStatus.AllPlayersReady;

          this.setState(prevState => ({
            match,
            activePlayer: playerInfo.id === match.currentPlayer ? 'player' : 'player2',
            player: { ...prevState.player, shipsSet: playerReady },
            allShipsSet: allPlayersReady,
            waitingAdversary: playerReady,
            gameStarted: allPlayersReady,
            gameOver: match.status === MatchStatus.Closed
          }));
        })
        .catch((e) => PopUp.showPopUp('error', 'Falha ao carregar dados da partida'))
        .finally(() => this.setState({ loadingMatch: false }))
    })
  }

  getPositions = () => {
    ApiClient.GetPositions()
      .then(({ battleFields }) => {
        console.log(battleFields);
      })
      .catch(() => PopUp.showPopUp('error', 'Falha ao obter posições do jogador'))
  }

  getOpponentData = () => {
    const opponentId = this.match?.adversary?.id;
    ApiClient.GetPositionsByPlayerId(opponentId)
      .then(({ battleFields }) => {
        console.log(battleFields)
      })
      .catch(() => PopUp.showPopUp('error', 'Falha ao obter posições do adversário'))
  }

  StartCheckingPlayerReady = () => {
    this.intervalId = setInterval(this.CheckPlayersReady, 1000);
  }

  StopCheckingPlayerReady = () => {
    clearInterval(this.intervalId);
  }

  CheckPlayersReady = () => {
    console.log('Checking all players ready...');

    let playersReady = localStorage.getItem('StartMatch');

    if (playersReady === 'true') {
      this.StopCheckingPlayerReady();

      const { player } = this.state;
      this.setState({ waitingAdversary: false, gameStarted: true, player: { ...player, shipsSet: true } });
      return;
    }

    console.log('All players not ready yet!');
  }

  addHandlersForBattle() {
    let global = this;

    global.removeHandlers();

    global.hubConnection.on("PlayerReady", function (partnerName, ships) {
      PopUp.showPopUp('success', `${partnerName} terminou de posicionar os navios`);
      localStorage.setItem('opponent-ships', ships);
      global.setState({ player2: createPlayer(JSON.parse(ships)) });
    });

    global.hubConnection.on("StartGame", function (currentPlayerId) {
      const playerInfo = UserService().getPlayerData();

      localStorage.setItem('StartMatch', true)
      this.setState({ activePlayer: playerInfo.id === currentPlayerId ? 'player' : 'player2' })
    });

    global.hubConnection.on("TakeShoot", function (x, y, specialPowerPositions, hitTarget, winner) {
      let player = global.state["player"];

      player.shipsGrid[x][y].status = hitTarget ? "hit" : "miss";
      
      const playerShip = player
      .ships
      .find(({ positions }) => positions.find(position => position.row === x && position.col === y));

      specialPowerPositions.forEach(({ row: sRow,  col: sCol }) => {
        const hitOpponentShip = playerShip.positions.find(position => position.row === sRow && position.col === sCol)
    
        if (hitOpponentShip) {
          player.shipsGrid[sRow][sCol].status = "hit";
          playerShip.positions.forEach(position => {
            if (position.row === sRow && position.col === sCol) {
              position.hit = true;
            }
          });
        } else {
          player.shipsGrid[sRow][sCol].status = "miss";
        }
      })

      global.setState({
        player: player,
        activePlayer: hitTarget ? 'player2' : 'player',
        gameOver: winner !== null,
        winner
      });
    });
  }

  removeHandlers() {
    this.hubConnection.off("PlayerReady");
    this.hubConnection.off("StartGame");
    this.hubConnection.off("TakeShoot");
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

  updateGrids(player, grid, type, opponent, hitTarget) {
    const payload = {
      player,
      grid,
      type,
      opponent
    }
    this.gridReducer("UPDATE", payload);
    if (opponent && opponent.sunkenShips === 5) {
      this.gridReducer("GAME_OVER", payload);
    } else if (opponent && hitTarget) {
      this.setState({ currentSpecialPower: null });
      this.gridReducer("HIT", payload);
    } else if (opponent && !hitTarget) {
      this.gridReducer("MISS", payload);
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
        winner: UserService().getPlayerData().id
      });
    }
    if (action === "HIT") {
      this.setState({
        [other]: opponent
      });
    }
    if (action === "MISS") {
      this.setState({
        [other]: opponent,
        activePlayer: other
      });
    }
  }

  handleSpecialPower = (specialPower) => this.setState({ currentSpecialPower: specialPower })

  renderBattleGrid(player) {
    const opponent = "player2";
    const { activePlayer, currentSpecialPower } = this.state;
    return (
      <BattleGrid
        player={player}
        grid={this.state[player].movesGrid}
        opponent={this.state[opponent]}
        updateGrids={this.updateGrids}
        activePlayer={activePlayer}
        shipsSet={this.state[player].shipsSet}
        websocketTakeShot={WebSocketHandler.TakeShot}
        matchInfo={this.match}
        currentSpecialPower={currentSpecialPower}
      />
    );
  }

  renderShipGrid(player) {
    const { activePlayer, gameOver, themeShips, currentSpecialPower } = this.state;

    return (
      <ShipGrid
        player={player}
        grid={this.state[player].shipsGrid}
        ships={this.state[player].ships}
        currentShip={this.state[player].currentShip}
        themeShips={themeShips}
        updateGrids={this.updateGrids}
        updateShips={this.updateShips}
        handlePowerChoose={this.handleSpecialPower}
        currentSpecialPower={currentSpecialPower}
        shipsSet={this.state[player].shipsSet}
        allShipsSet={this.state.allShipsSet}
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
        <h5><b>Precione a tecla 'G' para girar os barcos!</b></h5>
      </div>
    )
  }

  updateShips(player, updatedShips) {
    const { ships, currentShip } = this.state[player];
    const payload = {
      updatedShips,
      player
    }
    if (currentShip + 1 === ships.length) {
      this.shipReducer("START_GAME", payload);
    } else {
      this.shipReducer("SET_SHIP", payload)
    }
  }

  shipReducer(action, { updatedShips, player }) {
    const { currentShip, ships } = this.state[player];
    const positionedShips = ships.filter(ship => ship.positions.length > 0);

    if (action === "START_GAME") {
      this.setState({
        activePlayer: "player",
        gameStarting: true,
        allShipsSet: true
      });
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
    localStorage.setItem('player-ships', JSON.stringify(ships));

    const match = JSON.parse(localStorage.getItem('match'));
    const matchId = match.matchId;

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
      .then(() => {
        this.setState({ waitingAdversary: true })
        WebSocketHandler.PlayerReady(match.adversary.code, match.player.name, match.player.code, JSON.stringify(ships));
        this.StartCheckingPlayerReady();
      })
      .catch(() => PopUp.showPopUp('error', 'Falha ao enviar posições para o servidor'))
  }

  render() {
    const { themes, themeShips, loadingMatch, loadingTheme, themeSelected, player, waitingAdversary,
      gameStarted, gameOver, winner } = this.state;
    const positionedAllShips = player.ships.every(ship => ship.positions.length > 0);
    const win = winner === UserService().getPlayerData().id;

    return (
      <Fragment>
        <NavBar />
        {loadingMatch && <div><h1>Carregando dados da partida...</h1></div>}
        {gameOver && (
          <div>
            <h1 className="center">Jogo encerrado!</h1>
            {win ? <h3 style={{ marginLeft: "20%" }}><b>Parabéns!</b> Você venceu a partida!</h3> : <h3 style={{ marginLeft: "20%" }}>Que pena... você perdeu a partida</h3>}
            <br />
            <LinkWrapper to="/Home" className="waves-effect waves-light btn" style={{ color: "white", marginLeft:"10%", backgroundColor: "rgba(51,51,51,0.08)" }} activeStyle={{}}>
              <i className="material-icons left">arrow_back</i>
              Voltar para início
            </LinkWrapper>
          </div>
        )}
        {!gameOver && !loadingMatch && !themeSelected && (
          <div>
            <h2 className="center">Escolha o Tema</h2>
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
        {!gameOver && !loadingMatch && themeSelected && (
          <div className="game">
            <img className="battlefield-background" src={theme.imagePath} />
            <div className="row">
              <div className="col l6 s12" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                {themeShips.length > 0 && this.renderShipGrid("player")}
                {!gameStarted && (
                  <Button onClick={() => this.startBattleShip()} disabled={!positionedAllShips || waitingAdversary}>
                    {waitingAdversary ? 'Aguardando adversário' : 'PRONTO'}
                  </Button>
                )}
              </div>
              <div className="col l6 s12" style={gameStarted ? { display: 'flex', flexDirection: 'column', alignItems: 'center' } : {}}>
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