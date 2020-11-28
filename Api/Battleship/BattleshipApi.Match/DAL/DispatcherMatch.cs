using BattleshipApi.Base.DAL;
using BattleshipApi.Match.DML.Enumerados;
using BattleshipApi.Match.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
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
        /// Update the match and returns your ID
        /// </summary>
        /// <param name="pMatch">Match Object</param>
        /// <returns>Número da partida</returns>
        public void UpdateMatch(DML.Match match)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INT,   ");
            stringBuilder.AppendLine("  @PLAYER1READY INT,");
            stringBuilder.AppendLine("  @PLAYER2READY INT,");
            stringBuilder.AppendLine("  @STATUS INT");

            stringBuilder.AppendLine($"SET @ID = {match.ID}");
            stringBuilder.AppendLine($"SET @PLAYER1READY = '{match.Player1Ready}'");
            stringBuilder.AppendLine($"SET @PLAYER2READY = '{match.Player2Ready}'");
            stringBuilder.AppendLine($"SET @STATUS = '{Convert.ToInt32(match.Status)}'");

            stringBuilder.AppendLine("UPDATE Match");
            stringBuilder.AppendLine("SET");
            stringBuilder.AppendLine("  Player1Ready = @PLAYER1READY,");
            stringBuilder.AppendLine("  Player2Ready = @PLAYER2READY,");
            stringBuilder.AppendLine("  Status = @STATUS");

            IUnitOfWork.Executar(stringBuilder.ToString());
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
            stringBuilder.AppendLine("  PLAYER1READY,");
            stringBuilder.AppendLine("  PLAYER2READY,");
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

                if (ds.Tables[0].Rows[0]["PLAYER1READY"] != DBNull.Value)
                    partida.Player1Ready = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER1READY"]);

                if (ds.Tables[0].Rows[0]["PLAYER2READY"] != DBNull.Value)
                    partida.Player2Ready = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER2READY"]);

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
            query.AppendLine("SET CURRENTPLAYER = @CURRENT,");
            query.AppendLine("  MatchContrl  = ISNULL(MatchContrl ,1) + 1");
            query.AppendLine("WHERE ID = @MATCH");

            IUnitOfWork.Executar(query.ToString());
        }

        public DML.Match Get(int ID)
        {
            return Get().FirstOrDefault(match => match.ID == ID);
        }

        public List<DML.Match> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SELECT ID, Player1, Player2, Player1Ready, Player2Ready, Status, CurrentPlayer, Winner, MatchContrl FROM Match WITH(NOLOCK)");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<DML.Match> matchs = new List<DML.Match>();

                DML.Match match;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    match = new DML.Match();

                    if (row["ID"] != DBNull.Value)
                        match.ID = Convert.ToInt32(row["ID"]);
                    if (row["Player1"] != DBNull.Value)
                        match.Player1 = Convert.ToInt32(row["Player1"]);
                    if (row["Player2"] != DBNull.Value)
                        match.Player2 = Convert.ToInt32(row["Player2"]);
                    if (row["Player1Ready"] != DBNull.Value)
                        match.Player1Ready = Convert.ToInt32(row["Player1Ready"]);
                    if (row["Player2Ready"] != DBNull.Value)
                        match.Player2Ready = Convert.ToInt32(row["Player2Ready"]);
                    if (row["Status"] != DBNull.Value)
                        match.Status = (MatchStatus)Convert.ToInt32(row["Status"]);
                    if (row["CurrentPlayer"] != DBNull.Value)
                        match.CurrentPlayer = Convert.ToInt32(row["CurrentPlayer"]);
                    if (row["Winner"] != DBNull.Value)
                        match.Winner = Convert.ToInt32(row["Winner"]);
                    if (row["MatchContrl"] != DBNull.Value)
                        match.Controle = Convert.ToInt32(row["MatchContrl"]);

                    matchs.Add(match);
                }
                return matchs;
            }
            return new List<DML.Match>();
        }
    }
}
