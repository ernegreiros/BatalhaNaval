using System.Collections.Generic;
using BattleshipApi.Models;

namespace Battleship.Models.BattleField.Out
{
    public class OutGetBattleFieldsVM : OutBase
    {
        public List<BattleshipApi.BattleField.DML.BattleField> BattleFields { get; internal set; } = new List<BattleshipApi.BattleField.DML.BattleField>();
    }
}
