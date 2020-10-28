using BattleshipApi.Match.DML;
using BattleshipApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Match.Out
{
    public class OutCurrentMatchVM : OutBase
    {
        public OutCurrentMatchVM()
        {
            Match = new BattleshipApi.Match.DML.Match();
        }

        public BattleshipApi.Match.DML.Match Match { get; set; }
    }
}
