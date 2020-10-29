using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DML.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IBoBattleField
    {
        void RegisterPositions(List<BattleField> pBattleFieldsPositions);
        int AttackPositions(List<BattleField> pBattleFieldsPositions);
    }
}
