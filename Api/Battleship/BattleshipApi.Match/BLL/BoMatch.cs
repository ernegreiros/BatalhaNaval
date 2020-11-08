using BattleshipApi.Player.DML.Interfaces;
using BattleshipApi.Match.DML.Interfaces;
using System;
using System.Linq;
using DataBaseHelper.Interfaces;
using BattleshipApi.MatchSpecialPower.DML.Interfaces;

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
        /// <param name="pIBoMatchSpecialPower">Business class of match/special power</param>
        public BoMatch(IBoPlayer pIBOPlayer, IDispatcherMatch pIDispatcherMatch, IBoMatchSpecialPower pIBoMatchSpecialPower)
        {
            IBoJogador = pIBOPlayer;
            IDispatcherMatch = pIDispatcherMatch;
            IBoMatchSpecialPower = pIBoMatchSpecialPower;
        }

        /// <summary>
        /// Business class of player
        /// </summary>
        private readonly IBoPlayer IBoJogador;

        /// <summary>
        /// Connection class to the database for match
        /// </summary>
        private readonly IDispatcherMatch IDispatcherMatch;

        /// <summary>
        /// Business object
        /// </summary>
        private readonly IBoMatchSpecialPower IBoMatchSpecialPower;

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

            try
            {

                IDispatcherMatch.BeginTransaction();
                int matchId = IDispatcherMatch.CreateMatch(pMatch);
                IBoMatchSpecialPower.RegisterSpecialPowerToMatch(matchId);
                IDispatcherMatch.Commit();
                return matchId;

            }
            catch (Exception)
            {
                IDispatcherMatch.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Search the player's current game (With status started)
        /// </summary>
        /// <param name="pUserName">User name</param>
        /// <returns>Partida atual</returns>
        public DML.Match CurrentMatch(string pUserName)
        {
            if (string.IsNullOrEmpty(pUserName))
                throw new ArgumentNullException(paramName: nameof(pUserName), message: "User name is required");

            Player.DML.Player player = IBoJogador.FindPlayerByUserName(pUserName);

            return CurrentMatch(player.ID);
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
                return IDispatcherMatch.CurrentMatch(pPlayerID);
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

            IDispatcherMatch.CloseMatch(pMatchId);
        }

        public void ChangeCurrentPlayer(int pMatchId, int pCurrentPlayer)
        {
            if (pMatchId <= 0)
                throw new Exception("Id da partida é obrigatório");

            if (pCurrentPlayer <= 0)
                throw new Exception("Current player id required");

            IDispatcherMatch.ChangeCurrentPlayer(pMatchId, pCurrentPlayer);
        }
    }
}
