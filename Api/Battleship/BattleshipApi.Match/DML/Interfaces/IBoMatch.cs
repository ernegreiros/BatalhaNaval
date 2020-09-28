namespace BattleshipApi.Match.DML.Interfaces
{
    /// <summary>
    /// Business object interface of match
    /// </summary>
    public interface IBoMatch
    {
        /// <summary>
        /// Create a match and returns your id
        /// </summary>
        /// <param name="pMatch">Match object</param>
        /// <returns>Math id</returns>
        int CreateMatch(DML.Match pMatch);

        /// <summary>
        /// Search the player's current game (With status started)
        /// </summary>
        /// <param name="pPlayerID">Player ID</param>
        /// <returns>Partida atual</returns>
        DML.Match CurrentMatch(int pPlayerID);

        /// <summary>
        /// Close the match
        /// </summary>
        /// <param name="pMatchId">Match ID</param>
        void CloseMatch(int pMatchId);
    }
}
