using BattleshipApi.JWT.DML.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.JWT.DML
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
