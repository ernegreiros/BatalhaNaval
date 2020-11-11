using BattleshipApi.MatchAttacks.DML;
using BattleshipApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.BattleField.Out
{
    public class OutAttackPlayerVM : OutBase
    {
        public OutAttackPlayerVM()
        {
            PositionsAttacked = new List<MatchAttacks>();
        }

        public bool EnemyDefeated { get; set; }
        public bool HitTarget { get; set; }
        public List<MatchAttacks> PositionsAttacked { get; set; }
    }
}
