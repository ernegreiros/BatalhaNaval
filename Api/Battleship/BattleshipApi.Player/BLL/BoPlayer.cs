using BattleshipApi.Player.DML.Interfaces;
using System;
using System.Text;

namespace BattleshipApi.Player.BLL
{
    /// <summary>
    /// Business class of players
    /// </summary>
    public class BoPlayer : IBoPlayer
    {
        private readonly IDispatcherPlayer IDispatcherPlayer;

        public BoPlayer(IDispatcherPlayer pIDispatcherPlayer)
        {
            IDispatcherPlayer = pIDispatcherPlayer;
        }

        /// <summary>
        /// Check if password is match
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pPassword">Password</param>
        /// <returns></returns>
        public bool PasswordMatch(string pLogin, string pPassword)
        {
            if (string.IsNullOrEmpty(pLogin) || string.IsNullOrEmpty(pPassword))
                return false;
            return IDispatcherPlayer.PasswordMatch(pLogin, pPassword);
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

        public DML.Player GetPlayerInfo(int playerId)
        {
            if (playerId <= 0)
                throw new Exception("ID do jogador inválido");

            return IDispatcherPlayer.GetPlayerInfo(playerId);
        }

        public void InsertPlayer(DML.Player playerObject)
        {
            if (String.IsNullOrEmpty(playerObject.Name))
                throw new Exception("Nome do jogador necessário");

            IDispatcherPlayer.InsertPlayer(playerObject);
        }

        /// <summary>
        /// Find player by user name
        /// </summary>
        /// <param name="pUserName">User name</param>
        /// <returns>Player</returns>
        public DML.Player FindPlayerByUserName(string pUserName)
        {
            if (string.IsNullOrEmpty(pUserName))
                throw new ArgumentNullException(paramName: nameof(pUserName), message: "User name is required");

            return IDispatcherPlayer.FindPlayerByUserName(pUserName);
        }

        public void Update(DML.Player player)
        {
            if (player == null)
                throw new ArgumentNullException("Player is required");

            var oldPlayer = FindPlayerByUserName(player.Login);

            if (oldPlayer == null)
                throw new Exception("Player not found");

            oldPlayer.Name = player.Name ?? oldPlayer.Name;
            oldPlayer.Password = player.Password ?? oldPlayer.Password;

            IDispatcherPlayer.Update(oldPlayer);
        }
    }
}
