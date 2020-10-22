using BattleshipApi.Base.DAL.Interfaces;

namespace BattleshipApi.Player.DML.Interfaces
{
    /// <summary>
    /// Player database connection interface
    /// </summary>
    public interface IDispatcherPlayer : IDispatcherBase
    {
        /// <summary>
        /// Check if player exists
        /// </summary>
        /// <param name="pPlayerID">Player id</param>
        /// <returns>Check if player exists</returns>
        bool PlayerExists(int pPlayerID);
        Player GetPlayerInfo(int playerId);
        void InsertPlayer(Player playerObject);

        /// <summary>
        /// Find player by user name
        /// </summary>
        /// <param name="pUserName">User name</param>
        /// <returns>Player</returns>
        Player FindPlayerByUserName(string pUserName);
    }
}
