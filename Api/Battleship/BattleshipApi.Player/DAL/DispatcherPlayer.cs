using BattleshipApi.Base.DAL;
using BattleshipApi.Player.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Data;
using System.Text;

namespace BattleshipApi.Player.DAL
{
    /// <summary>
    /// Classe de comunicação com o banco de dados para assunto de jogador
    /// </summary>
    public class DispatcherPlayer : DispatcherBase, IDispatcherPlayer
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public DispatcherPlayer(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        /// <summary>
        /// Check if player exists
        /// </summary>
        /// <param name="pIdJogador">Player ID</param>
        /// <returns>Return if player exists</returns>
        public bool PlayerExists(int pIdJogador)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine($"SET @ID = {pIdJogador}");
            stringBuilder.AppendLine("SELECT TOP 1 1 FROM USERS WITH(NOLOCK) WHERE ID = @ID");
                    
            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        public DML.Player GetPlayerInfo(int playerId)
        {
            var row = IUnitOfWork.Consulta($"SELECT * FROM USERS WITH(NOLOCK) WHERE ID = {playerId}").Tables[0].Rows[0];

            var player = new DML.Player
            {
                ID = Convert.ToInt32(row["ID"]),
                Name = row["Name"].ToString(),
                Login = row["Login"].ToString(),
                Password = null,
                Code = row["Code"].ToString(),
                Money = Convert.ToDouble(row["Money"])
            };

            return player;
        }

        public void InsertPlayer(DML.Player playerObject)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(playerObject).ToString());
        }
    }
}