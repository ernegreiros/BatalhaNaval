import React, { Component } from "react";
import { Button, Modal, Table } from "react-materialize";
import ShipGridSquare from "./ShipGridSquare";

import { placeShip, hoverUpdate } from "../../Utils/shipGridHelpers";
import "../../styles/Grid.css";

import MoedaImage from '../../assets/moeda.png';
import ApiClient from "../../Repositories/ApiClient";
import PopUp from "../PopUp/PopUp";
import SpecialPowerTypes from "../../Enums/SpecialPowerTypes";

class ShipGrid extends Component {
  constructor(props) {
    super(props);

    this.state = {
      player: JSON.parse(localStorage.getItem('player')),
      ships: props.ships,
      rotated: true,
      activeSpot: null,
      specialPowers: [],
      choosingPower: false,
    };

    this.handleRotate = this.handleRotate.bind(this);
    this.handleHover = this.handleHover.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.rotateByKey = this.rotateByKey.bind(this);
  }

  componentDidMount() {
    document.addEventListener("keydown", this.rotateByKey, false);

    ApiClient.GetSpecialPowers()
      .then(specialPowers => this.setState({ specialPowers }))
      .catch(() => PopUp.showPopUp('error', 'Its not your turn!!'));
  }

  componentWillUnmount() {
    document.removeEventListener("keydown", this.rotateByKey, false);
  }

  rotateByKey(event) {
    if (event.keyCode === 71 || event.keyCode === 103) {
      const { grid } = this.props;

      grid.map((row, i) => {
        row.map((square, j) => {
          square.hover = false;
        })
      });

      this.handleRotate();
    }
  }

  renderSquares() {
    const { grid, shipsSet, gameOver } = this.props;

    return grid.map((row, i) => {
      return row.map((square, j) => {
        return (
          <ShipGridSquare
            key={`${i}${j}`}
            i={i}
            j={j}
            shipsSet={shipsSet}
            square={square}
            handleHover={this.handleHover}
            handleClick={this.handleClick}
          />
        );
      });
    });
  }

  handleRotate() {
    this.setState(prevState => {

      let shipsArray = prevState.ships;
      shipsArray[this.props.currentShip].vertical = !shipsArray[this.props.currentShip].vertical;

      return {
        rotated: !prevState.rotated,
        ships: shipsArray
      };
    });
  }

  handleHover(row, col, type) {
    const { grid, ships, currentShip, allShipsSet } = this.props;
    const { rotated } = this.state;
    const data = {
      grid: grid.slice(),
      rotated,
      row,
      col,
      type,
      ships,
      currentShip
    };

    if (!allShipsSet) {
      const updatedGrid = hoverUpdate(data);
      this.props.updateGrids(this.props.player, updatedGrid, "shipsGrid", false);
    }
  }

  handleClick(row, col) {
    const { grid, ships, currentShip, allShipsSet } = this.props;
    const { rotated } = this.state;
    const data = {
      grid: grid.slice(),
      rotated,
      row,
      col,
      ships,
      currentShip
    };

    if (!allShipsSet) {
      const gameUpdate = placeShip(data);
      if (gameUpdate) {
        this.props.updateGrids(this.props.player, gameUpdate.grid, "shipsGrid", false);
        this.props.updateShips(this.props.player, gameUpdate.ships, "shipsGrid", false);
        this.setState({
          rotated: true
        });
      }
    }
  }

  RotateImage = (CloudNaryimagePath) => {
    let separator = 'upload';
    let stringArray = CloudNaryimagePath.split(separator);
    stringArray[1] = `/a_90${stringArray[1]}`;

    return stringArray.join(separator);
  }

  handleSpecialPower = specialPower => {
    const { player } = this.state;

    if (specialPower.cost > player.money)
      return PopUp.showPopUp('error', 'Você não possui moedas suficientes para usar o  poder');

    this.setState({ choosingPower: false, player: { ...player, money: player.money - specialPower.cost } });
    this.props.handlePowerChoose(specialPower);
  }

  render() {
    const { rotated, ships, player, choosingPower, specialPowers } = this.state;
    const { themeShips, currentSpecialPower, activePlayer } = this.props;
    const positionedShips = ships.filter(ship => ship.positions.length > 0);
    const positionedAllShips = ships.every(ship => ship.positions.length > 0);
    const isPlayerTurn = this.props.player === activePlayer;

    return (
      <div className="grid-container">
        <div style={{ position: 'relative' }} className="grid-title-container">
          <div style={{ display: 'flex', position: 'absolute', top: -10, left: 0 }}>
            <img style={{ display: 'inline', width: 50 }} src={MoedaImage} />
            <span style={{ fontSize: 30 }}>{player.money}</span>
          </div>
          <h5 className="grid-title center">Seu Campo</h5>
          <Modal
            style={{ width: 700 }}
            fixedFooter={false}
            actions={[]}
            header="Selecionar - Poder especial"
            open={choosingPower}
            options={{
              dismissible: true,
              endingTop: '10%',
              inDuration: 250,
              onCloseStart: null,
              onOpenEnd: () => this.setState({ choosingPower: true }),
              onCloseEnd: () => this.setState({ choosingPower: false }),
              opacity: 0.5,
              outDuration: 250,
              preventScrolling: true,
              startingTop: '4%',
            }}
            trigger={<Button disabled={!isPlayerTurn || !!currentSpecialPower} style={{ position: 'absolute', top: -10, right: 0 }}>Poderes</Button>}
          >
            {choosingPower && (
              <Table style={{ margin: 20 }}>
                <thead>
                  <tr>
                    <th>Nome</th>
                    <th>Compensação vitória</th>
                    <th>Custo</th>
                    <th>Ações</th>
                  </tr>
                </thead>
                <tbody>
                  {specialPowers.map(specialPower => {
                    const { id, name, type, quantifier, compensation, cost } = specialPower;

                    return (
                      <tr key={id}>
                        <td>"{name}" - ({type === SpecialPowerTypes.Attack ? "Ataque" : "Defesa"} - {quantifier} campos)</td>
                        <td>{compensation}</td>
                        <td>{cost}</td>
                        <td><Button onClick={() => this.handleSpecialPower(specialPower)}>Usar</Button></td>
                      </tr>
                    );
                  })}
                </tbody>
              </Table>
            )}
          </Modal>
        </div>
        {currentSpecialPower && <b>Você está utilizando o poder: {currentSpecialPower.name} ({currentSpecialPower.type === SpecialPowerTypes.Attack ? "Ataque" : "Defesa"} - {currentSpecialPower.quantifier} campos)</b>}

        <div className="grid" style={{ position: "absolute" }}>{this.renderSquares()}</div>
        <div className="grid">
          {positionedShips.map((ship, index) => {
            const themeShip = themeShips.find(s => s.type === ship.size);
            const { 0: first, [ship.positions.length - 1]: last } = ship.positions;

            return (
              <img
                key={index}
                style={{
                  margin: "auto",
                  ...(!ship.vertical
                    ? { gridRow: first.row + 1, gridColumn: `${first.col + 1}/${last.col + 2}` }
                    : { maxWidth: "100%", height: "100%", gridRow: `${first.row + 1}/${last.row + 2}`, gridColumn: `${first.col + 1}/${last.col + 1}` })
                }}
                src={ship.vertical ? this.RotateImage(themeShip.imagePath) : themeShip.imagePath} />
            )
          })}
        </div>
        {positionedAllShips && (
          <h4 style={{ marginLeft: "2%" }}><b>{isPlayerTurn ? "É " : "Não é"} Seu Turno!</b></h4>
        )
        }
      </div>
    );
  }
}

export default ShipGrid;