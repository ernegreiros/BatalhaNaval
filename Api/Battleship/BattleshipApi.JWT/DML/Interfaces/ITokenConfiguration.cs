using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.JWT.DML.Interfaces
{
    public interface ITokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
