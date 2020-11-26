using BattleshipApi.Base.DAL;
using BattleshipApi.BattleField.DML;
using BattleshipApi.BattleField.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BattleshipApi.BattleField.DAL
{
    public class DispatcherBattleField : DispatcherBase, IDispatcherBattleField
    {
        public DispatcherBattleField(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public int AttackPosition(DML.BattleField pBattleFieldPosition)
        {
            int hited = 0;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @PLAYERID INT,");
            stringBuilder.AppendLine("  @POSX INT,");
            stringBuilder.AppendLine("  @POSY INT,");
            stringBuilder.AppendLine("  @MATCHID INT");
            stringBuilder.AppendLine($"SET  @MATCHID = {pBattleFieldPosition.MatchID}");
            stringBuilder.AppendLine($"SET  @POSX = {pBattleFieldPosition.PosX}");
            stringBuilder.AppendLine($"SET  @POSY = {pBattleFieldPosition.PosY}");
            stringBuilder.AppendLine($"SET  @PLAYERID = {pBattleFieldPosition.Player}");

            stringBuilder.AppendLine("SELECT 1 FROM BattleField");
            stringBuilder.AppendLine("WHERE POSX = @POSX AND POSY = @POSY");
            stringBuilder.AppendLine("AND ATTACKED = 0 AND MATCHID = @MATCHID");
            stringBuilder.AppendLine("AND PLAYERID <> @PLAYERID");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());
            stringBuilder.Clear();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                hited = 1;

            stringBuilder.AppendLine("DECLARE @PLAYERID INT,");
            stringBuilder.AppendLine("  @POSX INT,");
            stringBuilder.AppendLine("  @POSY INT,");
            stringBuilder.AppendLine("  @MATCHID INT");
            stringBuilder.AppendLine($"SET  @MATCHID = {pBattleFieldPosition.MatchID}");
            stringBuilder.AppendLine($"SET  @POSX = {pBattleFieldPosition.PosX}");
            stringBuilder.AppendLine($"SET  @POSY = {pBattleFieldPosition.PosY}");
            stringBuilder.AppendLine($"SET  @PLAYERID = {pBattleFieldPosition.Player}");

            stringBuilder.AppendLine("UPDATE BattleField");
            stringBuilder.AppendLine("SET ATTACKED = 1");
            stringBuilder.AppendLine("FROM Match A");
            /*LEFT JOIN PELAS POSIÇÕES E CONTROLE*/
            stringBuilder.AppendLine("LEFT JOIN BattleFieldDefended B ON A.ID = B.MatchId AND A.MatchContrl = B.MatchContrl AND POSX = @POSX AND POSY = @POSY AND PLAYERID <> @PLAYERID"); 
            stringBuilder.AppendLine("WHERE BattleField.POSX = @POSX AND BattleField.POSY = @POSY");
            stringBuilder.AppendLine("AND ATTACKED = 0 AND BattleField.MATCHID = @MATCHID");
            stringBuilder.AppendLine("AND BattleField.PLAYERID <> @PLAYERID");
            stringBuilder.AppendLine("AND ISNULL(B.ID,0) = 0"); /*Aonde não tenha posição defendida*/

            IUnitOfWork.Executar(stringBuilder.ToString());

            return hited;
        }

        public void DeffendPosition(DML.BattleFieldDefend p)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(p).ToString());
        }

        public List<DML.BattleField> Get(int playerID)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("DECLARE @PLAYERID INTEGER"); 
            stringBuilder.AppendLine($"SET @PLAYERID = {playerID}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  PLAYERID,");
            stringBuilder.AppendLine("  POSX,");
            stringBuilder.AppendLine("  POSY");
            stringBuilder.AppendLine("FROM BattleField WITH(NOLOCK)");
            stringBuilder.AppendLine("  WHERE PlayerID = @PLAYERID");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<DML.BattleField> battleFields = new List<DML.BattleField>();

                DML.BattleField battleField;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    battleField = new DML.BattleField();

                    if (row["ID"] != DBNull.Value)
                        battleField.Id = Convert.ToInt32(row["ID"]);
                    if (row["PLAYERID"] != DBNull.Value)
                        battleField.Player = Convert.ToInt32(row["PLAYERID"]);
                    if (row["POSX"] != DBNull.Value)
                        battleField.PositionObject.X = Convert.ToInt32(row["POSX"].ToString());
                    if (row["POSY"] != DBNull.Value)
                        battleField.PositionObject.Y = Convert.ToInt32(row["POSY"].ToString());

                    battleFields.Add(battleField);
                }

                return battleFields;
            }

            return new List<DML.BattleField>();
        }

        public bool PlayerDefeated(int pMatchId, int pTarget)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @TARGET INTEGER, @MATCHID INTEGER");
            query.AppendLine($"SET @TARGET = { pTarget }");
            query.AppendLine($"SET @MATCHID = {pMatchId}");
            query.AppendLine("SELECT TOP 1 1 FROM BattleField WITH(NOLOCK)");
            query.AppendLine("    WHERE MatchId = @MatchId");
            query.AppendLine("    AND PlayerId = @TARGET");
            query.AppendLine("    AND Attacked = 0"); /*Pegamos apenas o que não foi atacado, desse modo ainda sobrou algum barco*/

            DataSet ds = IUnitOfWork.Consulta(query.ToString());

            return !(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0);
        }

        public void RegisterPosition(DML.BattleField pBattleFieldsPosition)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pBattleFieldsPosition).ToString());
        }

        public DML.BattleField ShowPosition(BattleFieldDefend p)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DECLARE @PLAYERID INTEGER, @POSX INTEGER, @POSY INTEGER, @MATCHID INTEGER"); 
            query.AppendLine($"SET @playerid = {p.Player}");
            query.AppendLine($"SET @POSX = {p.PosX}");
            query.AppendLine($"SET @POSY = {p.PosY}");
            query.AppendLine($"SET @MATCHID = {p.MatchID}");
            query.AppendLine("SELECT * from BattleField");
            query.AppendLine("WHERE PLAYERID = @PLAYERID AND POSX = @POSX");
            query.AppendLine("    AND POSY = @POSY AND MATCHID = @MATCHID");

            DataSet ds = IUnitOfWork.Consulta(query.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                return new DML.BattleField()
                {
                    Attacked = Convert.ToInt32(row["Attacked"]),
                    MatchID = p.MatchID,
                    Player = p.Player,
                    PositionObject = new BattleFieldPosition()
                    {
                        X = p.PosX,
                        Y = p.PosY
                    }
                };
            }
            return null;
        }
    }
}
