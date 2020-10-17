using BattleshipApi.Models;
using BattleshipApi.Theme.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Themes.Out
{
    public class OutGetThemesVM : OutBase
    {
        public List<Theme> Themes { get; internal set; } = new List<Theme>();
    }
}
