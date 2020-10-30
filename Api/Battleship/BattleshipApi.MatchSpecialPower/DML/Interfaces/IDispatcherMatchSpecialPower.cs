﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchSpecialPower.DML.Interfaces
{
    public interface IDispatcherMatchSpecialPower
    {
        void RegisterSpecialPowerToMatch(int pMatchId);
        void RegisterUseOfSpecialPower(int pMatchId, int pPlayer, int pSpecialPower);
    }
}
