using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.Theme.DML.Interfaces
{
    public interface IBoTheme
    {
        void Create(Theme theme);
        void Delete(int pId);
        void Update(DML.Theme theme);
        Theme Get(int pId);
        List<Theme> Get();
    }
}
