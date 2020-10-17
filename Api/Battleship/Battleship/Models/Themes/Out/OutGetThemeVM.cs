using BattleshipApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Themes.Out
{
    public class OutGetThemeVM : OutBase
    {
        public BattleshipApi.Theme.DML.Theme Theme { get; set; } = new BattleshipApi.Theme.DML.Theme();
    }
}
