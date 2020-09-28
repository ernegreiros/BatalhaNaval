using BatalhaNavalApi.Player.DML.Interfaces;
using System;

namespace BatalhaNavalApi.Player.BLL
{
    /// <summary>
    /// Business class of players
    /// </summary>
    public class BoPlayer : IBoPlayer
    {
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
