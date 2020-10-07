using BattleshipApi.Base.DAL;
using BattleshipApi.BattleField.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.BattleField.DAL
{
    public class DispatcherBattleField : DispatcherBase, IDispatcherBattleField
    {
        public DispatcherBattleField(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public void RegisterPosition(DML.BattleField pBattleFieldsPosition)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pBattleFieldsPosition).ToString());
        }
    }
}
