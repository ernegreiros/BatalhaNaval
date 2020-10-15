using BattleshipApi.Ships.DML.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.Ships.BLL
{
    public class BoShips : IBoShips
    {
        private readonly IDispatcherShips IDispatcherShips;

        public BoShips(IDispatcherShips pIDispatcherShips)
        {
            IDispatcherShips = pIDispatcherShips;
        }

        public void Create(DML.Ships shipObject)
        {
            if (shipObject is null)
                throw new ArgumentNullException("Ship cannot be null");

            shipObject.CheckData();

            IDispatcherShips.Create(shipObject);
        }

        public void Delete(int shipId)
        {
            if (shipId <= 0)
                throw new ArgumentNullException("Invalid Id");

            IDispatcherShips.Delete(shipId);
        }

        public DML.Ships Get(int shipId)
        {
            if (shipId <= 0)
                throw new ArgumentNullException("Invalid Id");

            return IDispatcherShips.Get(shipId);
        }

        public void Update(DML.Ships shipObject)
        {
            if (shipObject.ID <= 0)
                throw new ArgumentNullException("Invalid Id");

            IDispatcherShips.Update(shipObject);
        }
    }
}
