using BattleshipApi.MatchAttacks.DML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipApi.MatchAttacks.BLL
{
    public class BoMatchAttacks : IBoMatchAttacks
    {
        private readonly IDispatcherMatchAttacks IDispatcherMatchAttacks;

        public BoMatchAttacks(IDispatcherMatchAttacks iDispatcherMatchAttacks)
        {
            IDispatcherMatchAttacks = iDispatcherMatchAttacks;
        }

        public void RegisterMatchAttacks(List<DML.MatchAttacks> pMatchAttacks)
        {
            if (pMatchAttacks.Any())
            {
                pMatchAttacks.ForEach((m) => m.CheckData());
                pMatchAttacks.ForEach((m) => { IDispatcherMatchAttacks.RegisterMatchAttacks(m); });
            }
        }
    }
}
