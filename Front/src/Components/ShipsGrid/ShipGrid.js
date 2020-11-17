import React, { Component } from "react";
import { placeShip, hoverUpdate } from "../../Utils/shipGridHelpers";
import ShipGridSquare from "./ShipGridSquare";
import "../../styles/Grid.css";

class ShipGrid extends Component {
  constructor(props) {
    super(props);

    this.state = {
      rotated: true,
      activeSpot: null
    };

    this.handleHover = this.handleHover.bind(this);
    this.handleClick = this.handleClick.bind(this);
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
      this.props.updateGrids(this.props.player, updatedGrid, "shipsGrid");
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
        this.props.updateGrids(this.props.player, gameUpdate.grid, "shipsGrid");
        this.props.updateShips(this.props.player, gameUpdate.ships, "shipsGrid");
      }
    }
  }

  render() {
    const { rotated } = this.state;
    const { themeShips, ships } = this.props;
    const positionedShips = ships.filter(ship => ship.positions.length > 0);

    return (
      <div className="grid-container">
        <h5 className="grid-title center"> Seu Campo </h5>
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
                  ...(rotated
                    ? { gridRow: first.row + 1, gridColumn: `${first.col + 1}/${last.col + 2}` }
                    : { transform: "rotate(90deg)", maxWidth: "none", maxHeight: "100%", gridRow: `${first.row + 1}/${last.row + 2}`, gridColumn: first.col + 1 })
                }}
                src={themeShip.imagePath} />
            )
          })}
        </div>
      </div>
    );
  }
}

export default ShipGrid;