import React, { Component } from "react";
import { hoverUpdate, placeMove } from '../../Utils/battleGridHelpers';
import PopUp from "../PopUp/PopUp";
import BattleGridSquare from "./BattleGridSquare";
import ApiClient from "../../Repositories/ApiClient";

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
    const { grid } = this.props;
    const { rotated } = this.state;
    const data = {
      grid: grid.slice(),
      rotated,
      row,
      col,
      type
    };
    const updatedGrid = hoverUpdate(data);
    this.props.updateGrids(this.props.player, updatedGrid, "movesGrid");
    this.setState({
      activeSpot: `${dictionary[col]}${row}`
    });
  }

  updateAttack = positions => ApiClient.AttackPositions(positions.map(({ row, col }) => ({ PosX: col, PosY: row })))

  async handleClick(row, col) {
    const { grid, opponent, player, activePlayer } = this.props;
    if (!activePlayer) {
      return;
    }


    // TODO: colocar de volta a alternação de ataque
    // if (player !== activePlayer) {
    //   return PopUp.showPopUp('error', 'Its not your turn!!');
    // }

    try {
      const attackResponse = await this.updateAttack([{ row, col }]);
      const { hitTarget, enemyDefeated, positionsAttacked = [] } = attackResponse;
      /*
          enemyDefeated: false
          hitTarget: true
          message: null
          positionsAttacked: []
       */

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

      const updatedGame = placeMove({ data, hitTarget, enemyDefeated, positionsAttacked });
      if (updatedGame) {
        this.props.updateGrids(this.props.player, updatedGame.grid, "movesGrid", updatedGame.opponent);
      }
    } catch (e) {
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
        <h5 className="grid-title center"> Campo do Adversário </h5>
        <div className="grid" onMouseLeave={this.handleExit}>
          {this.renderSquares()}
        </div>
        <div className="position">Active Spot: {this.state.activeSpot}</div>
      </div>
    );
  }
}

export default BattleGrid;