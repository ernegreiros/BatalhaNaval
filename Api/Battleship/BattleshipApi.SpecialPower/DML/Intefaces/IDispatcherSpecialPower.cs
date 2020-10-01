using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.DML.Intefaces
{
    public interface IDispatcherSpecialPower
    {
        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        void Create(DML.SpecialPower pSpecialPower);
    }
}
