using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Battleship
{
    public static class ClaimsExtensions
    {
        public static string GetJWTUserName(this IEnumerable<Claim> pClaims)
        {
            if (pClaims == null || !pClaims.Any())
                return string.Empty;            
            return pClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
        }
    }
}
