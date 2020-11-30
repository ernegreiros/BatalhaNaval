import React, { Component } from "react";
import { placeShip, hoverUpdate } from "../../Utils/shipGridHelpers";
import ShipGridSquare from "./ShipGridSquare";

import "../../styles/Grid.css";

import MoedaImage from '../../assets/moeda.png';

class ShipGrid extends Component {
  constructor(props) {
    super(props);

    this.state = {
      player: JSON.parse(localStorage.getItem('player')),
      ships: props.ships,
      rotated: true,
      activeSpot: null
    };

    this.handleRotate = this.handleRotate.bind(this);
    this.handleHover = this.handleHover.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.rotateByKey = this.rotateByKey.bind(this);
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

  componentDidMount() {
    document.addEventListener("keydown", this.rotateByKey, false);
  }

  componentWillUnmount() {
    document.removeEventListener("keydown", this.rotateByKey, false);
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
        this.props.updateGrids(this.props.player, gameUpdate.grid, "shipsGrid",false);
        this.props.updateShips(this.props.player, gameUpdate.ships, "shipsGrid",false);
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

  render() {
    const { rotated, ships, player } = this.state;
    const { themeShips } = this.props;
    const positionedShips = ships.filter(ship => ship.positions.length > 0);

    return (
      <div className="grid-container">
        <div style={{ position: 'relative' }} className="grid-title-container">
          <div style={{ display: 'flex', position: 'absolute', top: -10, left: 0 }}>
            <img style={{ display: 'inline', width: 50 }} src={MoedaImage} />
            <span style={{ fontSize: 30 }}>{player.money}</span>
          </div>
          <h5 className="grid-title center">Seu Campo</h5>
        </div>
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
      </div>
    );
  }
}

export default ShipGrid;