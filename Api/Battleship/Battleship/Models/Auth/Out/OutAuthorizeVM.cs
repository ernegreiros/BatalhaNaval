using BattleshipApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Auth.Out
{
    public class OutAuthorizeVM : OutBase
    {
        public string Token { get; set; }
    }
}
