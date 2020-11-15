using BattleshipApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.BattleField.Out
{
    public class OutShowPositionsVM : OutBase
    {
        public OutShowPositionsVM()
        {
            Positions = new List<BattleshipApi.BattleField.DML.BattleField>();
        }
        
        public List<BattleshipApi.BattleField.DML.BattleField> Positions { get; set; }
    }
}
