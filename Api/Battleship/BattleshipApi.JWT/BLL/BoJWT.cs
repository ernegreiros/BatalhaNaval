using BattleshipApi.JWT.DML;
using BattleshipApi.JWT.DML.Interfaces;
using BattleshipApi.Player.DML.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BattleshipApi.JWT.BLL
{
    public class BoJWT : IBoJWT
    {
        #region Const

        /// <summary>
        /// Name of policy
        /// </summary>
        public const string NormalUserPolicyName = "Bearer";

        /// <summary>
        /// Name of policy
        /// </summary>
        public const string SuperUserPolicyName = "SUPERUSER";

        /// <summary>
        /// Name of claim
        /// </summary>
        public const string SuperUserClaimName = "SUPERUSER";

        /// <summary>
        /// Super user login
        /// </summary>
        private const string SuperUserLogin = "admin@bn.com";

        /// <summary>
        /// Super user password
        /// </summary>
        private const string SuperUserPassword = "Admin";
        #endregion

        #region Readonly
        private readonly ITokenConfiguration ITokenConfiguration;
        private readonly ISigningConfigurations ISigningConfigurations;
        private readonly IBoPlayer IBoPlayer;
        #endregion

        #region Constructor
        public BoJWT(ITokenConfiguration iTokenConfiguration, ISigningConfigurations iSigningConfigurations, IBoPlayer iBoPlayer)
        {
            ITokenConfiguration = iTokenConfiguration;
            ISigningConfigurations = iSigningConfigurations;
            IBoPlayer = iBoPlayer;
        }
        #endregion

        public string WriteToken(AuthenticationData pModel)
        {
            pModel.CheckData();

            Claim[] cliams;

            /*Set superuser claim*/
            if (pModel.Login == SuperUserLogin && pModel.Password == SuperUserPassword)
            {
                cliams = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, pModel.Login),
                        new Claim(SuperUserClaimName, SuperUserClaimName)
                    };
            }
            else
            {
                if (IBoPlayer.PasswordMatch(pModel.Login, pModel.Password))
                    cliams = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, pModel.Login)
                    };
                else
                    throw new Exception("Password does not match");
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    new GenericIdentity(pModel.Login, "Login"),
                    cliams
                );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromMinutes(ITokenConfiguration.Minutes);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = ITokenConfiguration.Issuer,
                Audience = ITokenConfiguration.Audience,
                SigningCredentials = ISigningConfigurations.SigningCredentials,
                Subject = claimsIdentity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            return handler.WriteToken(securityToken);
        }
    }
}
