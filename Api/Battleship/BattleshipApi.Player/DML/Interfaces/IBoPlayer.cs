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
        Player GetPlayerInfo(int playerId);
        void InsertPlayer(Player playerObject);

        /// <summary>
        /// Check if password is match
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pPassword">Password</param>
        /// <returns></returns>
        bool PasswordMatch(string pLogin, string pPassword);
    }
}
