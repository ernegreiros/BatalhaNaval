using BattleshipApi.SpecialPower.DML.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.SpecialPower.BLL
{
    /// <summary>
    /// Special power business class
    /// </summary>
    public class BoSpecialPower : IBoSpecialPower
    {

        #region Readonly
        private readonly IDispatcherSpecialPower IDispatcherSpecialPower;
        #endregion

        public BoSpecialPower(IDispatcherSpecialPower pIDispatcherSpecialPower)
        {
            IDispatcherSpecialPower = pIDispatcherSpecialPower;
        }

        /// <summary>
        /// Create a special power
        /// </summary>
        /// <param name="pSpecialPower">Special power</param>
        public void Create(DML.SpecialPower pSpecialPower)
        {
            if (pSpecialPower == null)
                throw new ArgumentNullException("Special power cannot be null");
            pSpecialPower.CheckData();

            IDispatcherSpecialPower.Create(pSpecialPower);
        }
    }
}
