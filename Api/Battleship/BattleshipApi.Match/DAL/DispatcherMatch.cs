using BattleshipApi.Base.DAL;
using BattleshipApi.Match.DML.Enumerados;
using BattleshipApi.Match.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Text;

namespace BattleshipApi.Match.DAL
{
    /// <summary>
    /// Connection object of match
    /// </summary>
    public class DispatcherMatch : DispatcherBase, IDispatcherMatch
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIUnitOfWork">Connection object</param>
        public DispatcherMatch(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        /// <summary>
        /// Create the match and returns your ID
        /// </summary>
        /// <param name="pMatch">Match Object</param>
        /// <returns>Número da partida</returns>
        public int CreateMatch(DML.Match pMatch)
        {
            /*Inserindo no banco*/
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pMatch).ToString());
            /*Retornando ID da partida*/
            return CurrentMatch(pMatch.Player1).ID;
        }

        /// <summary>
        /// Search the player's current game (With status started)
        /// </summary>
        /// <param name="pPlayerID">Player ID</param>
        /// <returns>Partida atual</returns>
        public DML.Match CurrentMatch(int pPlayerID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_JOGADOR INT");
            stringBuilder.AppendLine($"SET @ID_JOGADOR = {pPlayerID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  PLAYER1,");
            stringBuilder.AppendLine("  PLAYER2,");
            stringBuilder.AppendLine("  STATUS");
            stringBuilder.AppendLine("FROM MATCH WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE PLAYER1 = @ID_JOGADOR OR PLAYER2 = @ID_JOGADOR");
            stringBuilder.AppendLine($"AND STATUS <> {Convert.ToInt32(MatchStatus.Closed)}");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());
            DML.Match partida = new DML.Match();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != DBNull.Value)
                    partida.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);

                if (ds.Tables[0].Rows[0]["PLAYER1"] != DBNull.Value)
                    partida.Player1 = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER1"]);

                if (ds.Tables[0].Rows[0]["PLAYER2"] != DBNull.Value)
                    partida.Player2 = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER2"]);

                if (ds.Tables[0].Rows[0]["STATUS"] != DBNull.Value)
                    partida.Status = (MatchStatus)Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"]);

                return partida;
            }
            return null;
        }

        /// <summary>
        /// Close the match
        /// </summary>
        /// <param name="pMatchId">Match ID</param>
        public void CloseMatch(int pMatchID)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INTEGER, @WINNER INTEGER");
            stringBuilder.AppendLine($"SET @ID = {pMatchID}");
            stringBuilder.AppendLine("SET @WINNER = 0");

            stringBuilder.AppendLine("SELECT @WINNER = B.PlayerId");
            stringBuilder.AppendLine("FROM BattleField B WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE B.MatchId = @ID");
            stringBuilder.AppendLine("    AND EXISTS (");
            stringBuilder.AppendLine("        SELECT TOP 1 1 FROM BattleField X WITH(NOLOCK)");
            stringBuilder.AppendLine("        WHERE X.Attacked = 0 AND X.MatchId = B.MatchId");
            stringBuilder.AppendLine(")");

            stringBuilder.AppendLine($"UPDATE TABLE MATCH SET STATUS = {Convert.ToInt32(MatchStatus.Closed)},");
            stringBuilder.AppendLine("Winner = @WINNER");
            stringBuilder.AppendLine("WHERE ID = @ID");

            stringBuilder.AppendLine("DECLARE @REWARD FLOAT");
            stringBuilder.AppendLine("SET @REWARD = 300");
            stringBuilder.AppendLine("SELECT @REWARD = SUM(S.Compensation)");
            stringBuilder.AppendLine("FROM SpecialPowers S WITH(NOLOCK)");
            stringBuilder.AppendLine("INNER JOIN MatchSpecialPowers MS WITH(NOLOCK) ON MS.SpecialPowerId = S.ID");
            stringBuilder.AppendLine("WHERE MS.MatchId = @ID AND USED = 1 AND PLAYERID = @WINNER");

            stringBuilder.AppendLine("UPDATE USERS");
            stringBuilder.AppendLine("SET MONEY = ISNULL(MONEY,0) + @REWARD");
            stringBuilder.AppendLine("WHERE ID = @WINNER");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }

        public void ChangeCurrentPlayer(int pMatchId, int pCurrentPlayer)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @MATCH INTEGER, @CURRENT INTEGER");
            query.AppendLine($"SET @MATCH = {pMatchId}");
            query.AppendLine($"SET @CURRENT = {pCurrentPlayer}");
            query.AppendLine("UPDATE Match");
            query.AppendLine("SET CURRENTPLAYER = @CURRENT");
            query.AppendLine("WHERE ID = @MATCH");

            IUnitOfWork.Executar(query.ToString());
        }
    }
}
