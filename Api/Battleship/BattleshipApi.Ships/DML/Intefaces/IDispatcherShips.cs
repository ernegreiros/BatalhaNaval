using BattleshipApi.Base.DAL.Interfaces;

namespace BattleshipApi.Ships.DML.Intefaces
{
    public interface IDispatcherShips : IDispatcherBase
    {

        void Create(Ships shipObject);
        Ships Get(int shipId);
        void Update(Ships specialPower);
        void Delete(int shipId);
    }
}
