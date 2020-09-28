namespace BattleshipApi.Player.DML.Interfaces
{
    /// <summary>
    /// Player database connection interface
    /// </summary>
    public interface IDispatcherPlayer
    {
        /// <summary>
        /// Check if player exists
        /// </summary>
        /// <param name="pPlayerID">Player id</param>
        /// <returns>Check if player exists</returns>
        bool PlayerExists(int pPlayerID);
    }
}
