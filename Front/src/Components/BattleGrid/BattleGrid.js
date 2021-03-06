import React, { Component } from "react";
import { hoverUpdate, placeMove } from '../../Utils/battleGridHelpers';
import PopUp from "../PopUp/PopUp";
import BattleGridSquare from "./BattleGridSquare";
import ApiClient from "../../Repositories/ApiClient";
import UserService from "../../Services/UserService";

const dictionary = {
  0: null,
  1: "A",
  2: "B",
  3: "C",
  4: "D",
  5: "E",
  6: "F",
  7: "G",
  8: "H",
  9: "I",
  10: "J"
};

class BattleGrid extends Component {
  constructor(props) {
    super(props);

    this.state = {
      rotated: false,
      activeSpot: null
    };

    this.handleRotate = this.handleRotate.bind(this);
    this.handleHover = this.handleHover.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.handleExit = this.handleExit.bind(this);
  }

  handleHover(row, col, type) {
    const { grid, currentSpecialPower } = this.props;
    const { rotated } = this.state;
    const data = {
      grid: grid.slice(),
      positions: [{ row, col }, ...this.buildAttackPositions(row, col, currentSpecialPower)],
      rotated,
      type
    };
    const updatedGrid = hoverUpdate(data);
    this.props.updateGrids("player2", updatedGrid, "shipsGrid", true);
    this.setState({
      activeSpot: `${dictionary[col]}${row}`
    });
  }

  updateAttack = (positions, specialPowerId = null) => ApiClient.AttackPositions(positions.map(({ row, col }) => ({ PosX: col, PosY: row })), specialPowerId)

  buildAttackPositions = (row, col, currentSpecialPower) => {
    return currentSpecialPower !== null
      ? [...Array(currentSpecialPower.quantifier - 1).keys()].map(key => ({ row, col: col + key + 1 }))
      : [];
  }

  async handleClick(row, col) {
    const { grid, opponent, player, activePlayer, currentSpecialPower } = this.props;

    if (!activePlayer) {
      return;
    }

    if (player !== activePlayer) {
      return PopUp.showPopUp('error', 'Não é seu turno!!');
    }

    try {
      const specialPowerPositions = this.buildAttackPositions(row, col, currentSpecialPower);
      const positionsToAttack = [{ row, col }, [{ row, col }, ...specialPowerPositions]];

      const attackResponse = await this.updateAttack(positionsToAttack, currentSpecialPower?.id);
      const { hitTarget, enemyDefeated, positionsAttacked = [] } = attackResponse;

      const { rotated } = this.state;
      const data = {
        player,
        grid: grid.slice(),
        rotated,
        row,
        col,
        opponent
      };

      // TODO: esse placemove tem que contemplar ataque de varios quadrados!

      const updatedGame = placeMove({ data, hitTarget, enemyDefeated, specialPowerPositions });
      if (updatedGame) {
        const playerInfo = UserService().getPlayerData();
        this.props.updateGrids(this.props.player, updatedGame.grid, "movesGrid", updatedGame.opponent, hitTarget);
        this.props.websocketTakeShot(this.props.matchInfo.adversary.code, "TakeShoot", data.row, data.col,specialPowerPositions, hitTarget, enemyDefeated ? playerInfo.id : null);
      }
    } catch (e) {
      console.log(e)
      PopUp.showPopUp('error', 'Falha ao realizar ataque');
    }
  }

  handleRotate() {
    this.setState(prevState => {
      return {
        rotated: !prevState.rotated
      };
    });
  }

  handleExit() {
    this.setState({
      activeSpot: null
    });
  }

  renderSquares() {
    const { opponent, shipsSet } = this.props;
    const { shipsGrid: oppponentShipsGrid } = opponent;

    return oppponentShipsGrid.map((row, i) => {
      return row.map((square, j) => {
        return (
          <BattleGridSquare
            key={`${i}${j}`}
            i={i}
            j={j}
            square={square}
            shipsSet={shipsSet}
            handleHover={this.handleHover}
            handleClick={this.handleClick}
          />
        );
      });
    });
  }

  render() {
    return (
      <div className="grid-container">
        <div className="grid-title-container">
          <h5 className="grid-title center"> Campo do Adversário </h5>
        </div>
        <div className="grid" onMouseLeave={this.handleExit}>
          {this.renderSquares()}
        </div>
        <div className="position">Atacando em: {this.state.activeSpot}</div>
      </div>
    );
  }
}

export default BattleGrid;