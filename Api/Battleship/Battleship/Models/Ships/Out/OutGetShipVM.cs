using BattleshipApi.Models;

namespace Battleship.Models.Ships.Out
{
    public class OutGetShipVM : OutBase
    {
        public BattleshipApi.Ships.DML.Ships Ship  { get; set; }
    }
}
