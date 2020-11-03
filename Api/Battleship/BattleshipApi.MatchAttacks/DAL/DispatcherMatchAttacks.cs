using BattleshipApi.Base.DAL;
using BattleshipApi.MatchAttacks.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchAttacks.DAL
{
    public class DispatcherMatchAttacks : DispatcherBase, IDispatcherMatchAttacks
    {
        public DispatcherMatchAttacks(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public void RegisterMatchAttacks(DML.MatchAttacks pMatchAttack)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pMatchAttack).ToString());
        }
    }
}
