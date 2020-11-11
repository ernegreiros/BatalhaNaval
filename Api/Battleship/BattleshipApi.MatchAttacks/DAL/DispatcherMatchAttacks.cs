using BattleshipApi.Base.DAL;
using BattleshipApi.MatchAttacks.DML;
using BattleshipApi.MatchAttacks.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BattleshipApi.MatchAttacks.DAL
{
    public class DispatcherMatchAttacks : DispatcherBase, IDispatcherMatchAttacks
    {
        public DispatcherMatchAttacks(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        public List<DML.MatchAttacks> PositionsAttacked(int pMatchId, int pTarget)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @MATCH INTEGER, @TARGET INTEGER");
            query.AppendLine($"SET @MATCH = { pMatchId }");
            query.AppendLine($"SET @TARGET = { pTarget }");
            query.AppendLine("SELECT MatchId,");
            query.AppendLine("    PosX,");
            query.AppendLine("    PosY,");
            query.AppendLine("    Target");
            query.AppendLine("FROM MatchAttacks WITH(NOLOCK)");
            query.AppendLine("WHERE MATCHID = @MATCH AND TARGET = @TARGET");

            DataSet ds = IUnitOfWork.Consulta(query.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var retorno = new List<DML.MatchAttacks>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.MatchAttacks attacks = new DML.MatchAttacks()
                    {
                        MatchId = Convert.ToInt32(row["MatchId"]),
                        PosX = Convert.ToInt32(row["PosX"]),
                        PosY = Convert.ToInt32(row["PosY"]),
                        Target = Convert.ToInt32(row["Target"])
                    };                   
                    
                    retorno.Add(attacks);
                }

                return retorno;
            }
            return new List<DML.MatchAttacks>();
        }

        public void RegisterMatchAttacks(DML.MatchAttacks pMatchAttack)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pMatchAttack).ToString());
        }
    }
}
