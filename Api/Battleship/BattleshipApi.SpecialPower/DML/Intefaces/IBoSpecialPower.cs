using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.DML.Intefaces
{
    /// <summary>
    /// Special powers business class interface
    /// </summary>
    public interface IBoSpecialPower
    {
        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        void Create(SpecialPower pSpecialPower);
        DML.SpecialPower Get(int specialPowerId);
        List<SpecialPower> GetAll();
        void Update(SpecialPower specialPower);
        void Delete(int specialPowerId);
    }
}
