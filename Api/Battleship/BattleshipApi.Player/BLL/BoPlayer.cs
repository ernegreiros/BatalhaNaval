using BattleshipApi.Player.DML.Interfaces;
using System;

namespace BattleshipApi.Player.BLL
{
    /// <summary>
    /// Business class of players
    /// </summary>
    public class BoPlayer : IBoPlayer
    {
        public bool PasswordMatch(string pLogin, string pPassword)
        {
#warning IMPLEMENTAR
            return true;

        }

        /// <summary>
        /// Check if player exists
        /// </summary>
        /// <param name="pPlayerID">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        public bool PlayerExists(int pPlayerID)
        {
            if (pPlayerID <= 0)
                throw new Exception("ID do jogador inválido");

            return true;
        }

    }
}
