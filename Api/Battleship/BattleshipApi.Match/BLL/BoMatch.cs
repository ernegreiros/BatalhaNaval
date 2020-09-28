using BattleshipApi.Player.DML.Interfaces;
using BattleshipApi.Match.DML.Interfaces;
using System;

namespace BattleshipApi.Match.BLL
{
    /// <summary>
    /// Business object of match
    /// </summary>
    public class BoMatch : IBoMatch
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pIBOPlayer">Business class of player</param>
        /// <param name="pIDispatcherMatch">Connection class to the database for match</param>
        public BoMatch(IBoPlayer pIBOPlayer, IDispatcherMatch pIDispatcherMatch)
        {
            IBoJogador = pIBOPlayer;
            IDispatcherPartida = pIDispatcherMatch;
        }

        /// <summary>
        /// Business class of player
        /// </summary>
        private readonly IBoPlayer IBoJogador;

        /// <summary>
        /// Connection class to the database for match
        /// </summary>
        private readonly IDispatcherMatch IDispatcherPartida;

        /// <summary>
        /// Create a match and returns your id
        /// </summary>
        /// <param name="pMatch">Match object</param>
        /// <returns>Math id</returns>
        public int CreateMatch(DML.Match pMatch)
        {
            if (CurrentMatch(pMatch.Player1) != null)
                throw new Exception("Jogador 1 já possui uma partida iniciada");
            if (CurrentMatch(pMatch.Player2) != null)
                throw new Exception("Jogador 2 já possui uma partida iniciada");

            pMatch.StatusDaPartida = DML.Enumerados.MatchStatus.Created;
            return IDispatcherPartida.CurrentMatch(pMatch.Player1).ID;
        }

        /// <summary>
        /// Search the player's current game (With status started)
        /// </summary>
        /// <param name="pPlayerID">Player ID</param>
        /// <returns>Partida atual</returns>
        public DML.Match CurrentMatch(int pPlayerID)
        {
            if (IBoJogador.PlayerExists(pPlayerID))
            {
                return IDispatcherPartida.CurrentMatch(pPlayerID);
            }
            else
                throw new Exception("Jogador informado não cadastrado");
        }

        /// <summary>
        /// Close the match
        /// </summary>
        /// <param name="pMatchId">Match ID</param>
        public void CloseMatch(int pMatchId)
        {
            if (pMatchId <= 0)
                throw new Exception("Id da partida é obrigatório");

            IDispatcherPartida.CloseMatch(pMatchId);
        }
    }
}
