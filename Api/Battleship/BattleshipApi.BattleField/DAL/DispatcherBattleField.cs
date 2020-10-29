using BattleshipApi.Base.DAL;
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
            stringBuilder.AppendLine("WHERE POSX = @POSX AND POSY = @POSY");
            stringBuilder.AppendLine("AND ATTACKED = 0 AND MATCHID = @MATCHID");
            stringBuilder.AppendLine("AND PLAYERID <> @PLAYERID");

            IUnitOfWork.Executar(stringBuilder.ToString());

            return hited;
        }

        public void RegisterPosition(DML.BattleField pBattleFieldsPosition)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pBattleFieldsPosition).ToString());
        }
    }
}
