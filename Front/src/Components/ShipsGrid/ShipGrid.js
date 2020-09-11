import React, { Component } from "react";
import { placeShip, hoverUpdate } from "../../Utils/shipGridHelpers";
import ShipGridSquare from "./ShipGridSquare";
import "../../styles/Grid.css";

class ShipGrid extends Component {
    constructor(props) {
        super(props);

        this.state = {
            rotated: false,
            activeSpot: null
        };

        this.handleRotate = this.handleRotate.bind(this);
        this.handleHover = this.handleHover.bind(this);
        this.handleClick = this.handleClick.bind(this);
    }

    renderSquares() {
        const { activePlayer, player, grid, shipsSet, gameOver } = this.props;
        if (player === activePlayer || gameOver) {
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
        } else {
            return null;
        }
    }

    handleHover(row, col, type) {
        const { grid, ships, currentShip } = this.props;
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
        const updatedGrid = hoverUpdate(data);
        this.props.updateGrids(this.props.player, updatedGrid, "shipsGrid");
    }

    handleClick(row, col) {
        const { grid, ships, currentShip } = this.props;
        const { rotated } = this.state;
        const data = {
            grid: grid.slice(),
            rotated,
            row,
            col,
            ships,
            currentShip
        };
        const gameUpdate = placeShip(data);
        if (gameUpdate) {
            this.props.updateGrids(this.props.player, gameUpdate.grid, "shipsGrid");
            this.props.updateShips(this.props.player, gameUpdate.ships, "shipsGrid");
        }
    }

    handleRotate() {
        this.setState(prevState => {
            return {
                rotated: !prevState.rotated
            };
        });
    }

    renderPlacement() {
        const { activePlayer, player, ships, currentShip, shipsSet } = this.props;
        if (player === activePlayer && !shipsSet) {
            return (
                <p className="placement-text">
                    Now placing: {ships[currentShip].type} - size:{" "}
                    {ships[currentShip].size}
                </p>
            );
        } else {
            return null;
        }
    }

    render() {
        return (
            <div className="grid-container">
                <h5 className="grid-title center"> Seu Campo </h5>
                <div className="grid">{this.renderSquares()}</div>
                {this.renderPlacement()}
                <button className="btn-rotate" onClick={this.handleRotate}>
                    Rotate direction
                </button>
            </div>
        );
    }
}

export default ShipGrid;