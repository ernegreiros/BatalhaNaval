using BattleshipApi.Models;
using System.Collections.Generic;

namespace Battleship.Models.Ships.Out
{
    public class OutGetAllShipVM : OutBase
    {
        public List<BattleshipApi.Ships.DML.Ships> Ship  { get; set; }
    }
}
