using BattleshipApi.Base.DAL;
using BattleshipApi.SpecialPower.DML.Intefaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.DAL
{
    public class DispatcherSpecialPower : DispatcherBase, IDispatcherSpecialPower
    {
        public DispatcherSpecialPower(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        public void Create(DML.SpecialPower pSpecialPower)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pSpecialPower).ToString());
        }
    }
}
