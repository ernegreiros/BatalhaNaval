using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.JWT.DML.Interfaces
{
    public interface IBoJWT
    {
        string WriteToken(AuthenticationData pModel);
    }
}
