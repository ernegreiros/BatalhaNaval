namespace BatalhaNavalApi.Player.DML.Interfaces
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
    }
}
