using System.Collections.Generic;

namespace Battleship.Models.BattleField.In
{
    public class InAttackPositionsVm
    {
        public List<InAttackPlayerVM> attackPositions { get; set; }
        public int? specialPower { get; set; }
    }
}
