using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchAttacks.DML.Interfaces
{
    public interface IBoMatchAttacks
    {
        void RegisterMatchAttacks(List<MatchAttacks> pMatchAttacks);
    }
}
