namespace BattleshipApi.Player.DML.Interfaces
{
    /// <summary>
    /// Business interface for player business class
    /// </summary>
    public interface IBoPlayer
    {
        /// <summary>
        /// Check if player exists
        /// </summary>
        /// <param name="pPlayerID">Player id</param>
        /// <returns>Return if player exists</returns>
        bool PlayerExists(int pPlayerID);
        
        /// <summary>
        /// Player game info
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Player GetPlayerInfo(int playerId);
        
        /// <summary>
        /// Insert player
        /// </summary>
        /// <param name="playerObject">Player object</param>
        void InsertPlayer(Player playerObject);

        /// <summary>
        /// Check if password is match
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pPassword">Password</param>
        /// <returns></returns>
        bool PasswordMatch(string pLogin, string pPassword);
        
        /// <summary>
        /// Find player by user name
        /// </summary>
        /// <param name="pUserName">User name</param>
        /// <returns>Player</returns>
        Player FindPlayerByUserName(string pUserName);

        Player FindPlayerByCode(string pUserName);

        void Update(Player player);
    }
}
