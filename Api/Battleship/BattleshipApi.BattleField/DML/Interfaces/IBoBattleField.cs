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
        List<BattleField> Get(int playerID);
        void RegisterPositions(List<BattleField> pBattleFieldsPositions);
        int AttackPositions(List<BattleField> pBattleFieldsPositions, int? pSpecialPowerId, out bool enemyDefeated);
        void DeffendPositions(List<BattleField> lists, int pSpecialPower);
        List<BattleField> ShowPositions(List<BattleField> lists, int pSpecialPower);
    }
}
