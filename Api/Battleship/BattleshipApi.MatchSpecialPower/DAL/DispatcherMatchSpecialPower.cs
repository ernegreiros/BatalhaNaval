using BattleshipApi.Base.DAL;
using BattleshipApi.MatchSpecialPower.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipApi.MatchSpecialPower.DAL
{
    public class DispatcherMatchSpecialPower : DispatcherBase, IDispatcherMatchSpecialPower
    {
        public DispatcherMatchSpecialPower(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public void RegisterSpecialPowerToMatch(int pMatchId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            #region Query
            GetPlayersFromMatchQuery(pMatchId, stringBuilder);
            DataLoadOfSpecialPowersOnMatch(stringBuilder);
            #endregion

            IUnitOfWork.Executar(stringBuilder.ToString());
        }

        private static void DataLoadOfSpecialPowersOnMatch(StringBuilder stringBuilder)
        {

            /*Carga de poderes especiais para o jogador 1*/
            stringBuilder.AppendLine("INSERT INTO MatchSpecialPowers (");
            stringBuilder.AppendLine("    MatchId,");
            stringBuilder.AppendLine("    PlayerId,");
            stringBuilder.AppendLine("    SpecialPowerId,");
            stringBuilder.AppendLine("    Used");
            stringBuilder.AppendLine("    )");
            stringBuilder.AppendLine("SELECT @MATCH,");
            stringBuilder.AppendLine("    @PLAYER1,");
            stringBuilder.AppendLine("    S.ID,");
            stringBuilder.AppendLine("    0");
            stringBuilder.AppendLine("FROM SpecialPowers S WITH(NOLOCK)");

            /*Carga de poderes especiais para o jogador 2*/
            stringBuilder.AppendLine("INSERT INTO MatchSpecialPowers(");
            stringBuilder.AppendLine("    MatchId,");
            stringBuilder.AppendLine("    PlayerId,");
            stringBuilder.AppendLine("    SpecialPowerId,");
            stringBuilder.AppendLine("    Used");
            stringBuilder.AppendLine("    )");
            stringBuilder.AppendLine("SELECT @MATCH,");
            stringBuilder.AppendLine("    @PLAYER2,");
            stringBuilder.AppendLine("    S.ID,");
            stringBuilder.AppendLine("    0");
            stringBuilder.AppendLine("FROM SpecialPowers S WITH(NOLOCK)");
        }

        private static void GetPlayersFromMatchQuery(int pMatchId, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("DECLARE @MATCH INT,");
            stringBuilder.AppendLine("    @PLAYER1 INT,");
            stringBuilder.AppendLine("    @PLAYER2 INT");
            stringBuilder.AppendLine($"SET @MATCH = { pMatchId }");

            /*Buscando jogadores da partida*/
            stringBuilder.AppendLine("SELECT @PLAYER1 = PLAYER1,");
            stringBuilder.AppendLine("    @PLAYER2 = Player2");
            stringBuilder.AppendLine("FROM Match WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE ID = @MATCH");
            stringBuilder.AppendLine("");
        }

        public void RegisterUseOfSpecialPower(int pMatchId, int pPlayer, int pSpecialPower)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @MatchId INT, @PlayerId INT, @SpecialPowerId INT");
            stringBuilder.AppendLine($"SET @MatchId = { pMatchId }");
            stringBuilder.AppendLine($"SET @PlayerId = { pPlayer }");
            stringBuilder.AppendLine($"SET @SpecialPowerId = { pSpecialPower }");
            
            stringBuilder.AppendLine("UPDATE MatchSpecialPowers");
            stringBuilder.AppendLine("    SET USED = 1");
            stringBuilder.AppendLine("WHERE MatchId = @MatchId");
            stringBuilder.AppendLine("    AND PlayerId = @PlayerId");
            stringBuilder.AppendLine("    AND SpecialPowerId = @SpecialPowerId");

            stringBuilder.AppendLine("UPDATE A");
            stringBuilder.AppendLine("    SET Money = ISNULL(Money,0) - S.COST");
            stringBuilder.AppendLine("FROM Users A");
            stringBuilder.AppendLine("INNER JOIN MatchSpecialPowers M ON M.PlayerId = A.ID");
            stringBuilder.AppendLine("INNER JOIN SpecialPowers S ON S.ID = M.SpecialPowerId");
            stringBuilder.AppendLine("WHERE MatchId = @MatchId");
            stringBuilder.AppendLine("    AND A.ID = @PlayerId");
            stringBuilder.AppendLine("    AND SpecialPowerId = @SpecialPowerId");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }
    }
}
