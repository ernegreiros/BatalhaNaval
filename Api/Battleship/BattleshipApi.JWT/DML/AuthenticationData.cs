using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.JWT.DML
{
    public class AuthenticationData
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void CheckData()
        {
            if (string.IsNullOrEmpty(Login))
                throw new ArgumentNullException(paramName: nameof(Login), message: "Login is required");

            if (string.IsNullOrEmpty(Password))
                throw new ArgumentNullException(paramName: nameof(Password), message: "Password is required");
        }
    }
}
