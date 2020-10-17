using BattleshipApi.Models;

namespace Battleship.Models.SpecialPower.Out
{
    public class OutGetSpecialPowerVM : OutBase
    {
        public BattleshipApi.SpecialPower.DML.SpecialPower SpecialPower { get; set; }
    }
}
