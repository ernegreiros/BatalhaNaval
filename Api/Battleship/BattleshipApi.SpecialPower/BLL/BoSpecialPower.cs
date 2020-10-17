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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pIDispatcherSpecialPower"></param>
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

        public DML.SpecialPower Get(int specialPowerId)
        {
            if (specialPowerId <= 0)
                throw new ArgumentNullException("Invalid Id");

            return IDispatcherSpecialPower.Get(specialPowerId);
        }

        public List<DML.SpecialPower> GetAll()
        {
            return IDispatcherSpecialPower.GetAll();
        }

        public void Update(DML.SpecialPower specialPower)
        {
            if (specialPower.ID <= 0)
                throw new ArgumentNullException("Invalid Id");

            IDispatcherSpecialPower.Update(specialPower);
        }

        public void Delete(int specialPowerId)
        {
            if (specialPowerId <= 0)
                throw new ArgumentNullException("Invalid Id");

            IDispatcherSpecialPower.Delete(specialPowerId);
        }
    }
}
