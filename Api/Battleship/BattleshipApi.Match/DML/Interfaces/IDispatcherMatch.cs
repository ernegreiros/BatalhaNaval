using BattleshipApi.Base.DAL.Interfaces;

namespace BattleshipApi.Match.DML.Interfaces
{
    /// <summary>
    /// Connection object interface of match
    /// </summary>
    public interface IDispatcherMatch : IDispatcherBase
    {
        /// <summary>
        /// Create the match and returns your ID
        /// </summary>
        /// <param name="pMatch">Match Object</param>
        /// <returns>Número da partida</returns>
        int CreateMatch(DML.Match pPartida);

        /// <summary>
        /// Search the player's current game (With status started)
        /// </summary>
        /// <param name="pPlayerID">Player ID</param>
        /// <returns>Partida atual</returns>
        DML.Match CurrentMatch(int pIdJogador);

        /// <summary>
        /// Close the match
        /// </summary>
        /// <param name="pMatchId">Match ID</param>
        void CloseMatch(int pIdPartida);
    }
}
