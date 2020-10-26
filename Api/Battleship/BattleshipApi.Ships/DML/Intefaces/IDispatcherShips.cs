using BattleshipApi.Base.DAL.Interfaces;
using System.Collections.Generic;

namespace BattleshipApi.Ships.DML.Intefaces
{
    public interface IDispatcherShips : IDispatcherBase
    {

        void Create(Ships shipObject);
        Ships Get(int shipId);
        void Update(Ships specialPower);
        void Delete(int shipId);
        List<Ships> GetAll(int themeId);
    }
}
