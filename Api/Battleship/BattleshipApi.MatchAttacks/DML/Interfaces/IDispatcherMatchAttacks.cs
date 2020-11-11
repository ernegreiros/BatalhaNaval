using BattleshipApi.Base.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchAttacks.DML.Interfaces
{
    public interface IDispatcherMatchAttacks : IDispatcherBase
    {
        void RegisterMatchAttacks(MatchAttacks pMatchAttack);
        List<MatchAttacks> PositionsAttacked(int pMatchId, int pTarget);
    }
}
