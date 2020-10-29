using BattleshipApi.MatchSpecialPower.DML.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchSpecialPower.BLL
{
    public class BoMatchSpecialPower : IBoMatchSpecialPower
    {
        private readonly IDispatcherMatchSpecialPower IDispatcherMatchSpecialPower;

        public BoMatchSpecialPower(IDispatcherMatchSpecialPower iDispatcherMatchSpecialPower)
        {
            IDispatcherMatchSpecialPower = iDispatcherMatchSpecialPower;
        }

        public void RegisterSpecialPowerToMatch(int pMatchId)
        {
            if (pMatchId <= 0)
                throw new ArgumentOutOfRangeException("Match id is required");

            IDispatcherMatchSpecialPower.RegisterSpecialPowerToMatch(pMatchId);
        }

        public void RegisterUseOfSpecialPower(int pMatchId, int pPlayer, int pSpecialPower)
        {
            if (pMatchId <= 0)
                throw new ArgumentOutOfRangeException("Match id is required");

            if (pPlayer <= 0)
                throw new ArgumentOutOfRangeException("Player id is required");

            if (pSpecialPower <= 0)
                throw new ArgumentOutOfRangeException("Special power is required");

            IDispatcherMatchSpecialPower.RegisterUseOfSpecialPower(pMatchId, pPlayer, pSpecialPower);
        }
    }
}
