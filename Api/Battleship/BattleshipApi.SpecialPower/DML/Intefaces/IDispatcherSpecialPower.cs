using BattleshipApi.Base.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.DML.Intefaces
{
    public interface IDispatcherSpecialPower : IDispatcherBase
    {
        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        void Create(DML.SpecialPower pSpecialPower);
        DML.SpecialPower Get(int specialPowerId);
    }
}
