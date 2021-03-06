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
}

const hoverUpdate = ({ grid, positions, rotated, type }) => {
  const bool = type === "enter";

  positions.forEach(({ row, col }) => {
    const position = grid[row][col];
    position.hover = bool;
  })

  return grid;
};

const isSunk = (ship) => {
  return !(!ship || ship.positions.find(position => !position.hit));
}

const getOpponentShipIdx = (opponent, row, col) => {
  let idx = 0;
  for (let i = 0; i < opponent.ships.length; i++) {
    if (opponent.ships[i].type === opponent.shipsGrid[row][col].type) {
      idx = i;
    }
  }
  return idx; 
}

const placeMove = ({ data, hitTarget, enemyDefeated, specialPowerPositions }) => {
  const {grid, row, col, rotated, player, opponent} = data;

  if (grid[row][col].status !== "empty") {
    return null
  }

  grid[row][col].hover = false;

  let log = [];
  log.push(`${player} targeted ${dictionary[col]}${row}`)

  if (!hitTarget) {
    log.push("It's a miss");
    opponent.shipsGrid[row][col].status = "miss";
    grid[row][col].status = "miss";

    specialPowerPositions.forEach(({ row: sRow, col: sCol }) => {
      opponent.shipsGrid[sRow][sCol].status = "miss";
      grid[sRow][sCol].status = "miss";
    });

    return {grid, opponent, log}
  }

  const opponentShip = opponent
    .ships
    .find(({ positions }) => positions.find(position => position.row === row && position.col === col));

  log.push("It's a hit!")

  opponent.shipsGrid[row][col].status = "hit";

  opponentShip.positions.forEach(position => {
    if (position.row === row && position.col === col) {
      position.hit = true;
    }
  })

  specialPowerPositions.forEach(({ row: sRow,  col: sCol }) => {
    const hitOpponentShip = opponentShip.positions.find(position => position.row === sRow && position.col === sCol)

    opponent.shipsGrid[sRow][sCol].hover = false;

    if (hitOpponentShip) {
      opponent.shipsGrid[sRow][sCol].status = "hit";
      opponentShip.positions.forEach(position => {
        if (position.row === sRow && position.col === sCol) {
          position.hit = true;
        }
      });
    } else {
      opponent.shipsGrid[sRow][sCol].status = "miss";
    }
  })

  if (isSunk(opponentShip)) {
    opponent.sunkenShips++;

    opponentShip.positions.forEach(position => {
      const { row, col } = position;
      opponent.shipsGrid[row][col].status = "sunk";
      grid[row][col].status = "sunk";
    });

    log.push(`${player} sank a ${opponentShip.type}!`)
  }

  if (opponent.sunkenShips === 5) {
    log.push(`${player} wins!`);
  }

  return {
    grid,
    opponent,
    log
  } 
};

const classUpdate = square => {
  let classes = "grid-square ";
  if (square.status !== "empty" && square.hover) {
    classes += "active-occupied";
  } else if (square.hover) {
    classes += "active";
  } else if (square.status === "hit") {
    classes += "enemy-hit";
  } else if (square.status === "miss") {
    classes += "enemy-miss";
  } else if (square.status === "sunk") {
    classes += "enemy-sunk";
  }
  return classes;
};

module.exports = {
  placeMove,
  hoverUpdate,
  classUpdate 
}