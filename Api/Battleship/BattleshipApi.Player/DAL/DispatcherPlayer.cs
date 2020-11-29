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

        /// <summary>
        /// Find player by user name
        /// </summary>
        /// <param name="pUserName">User name</param>
        /// <returns>Player</returns>
        public DML.Player FindPlayerByUserName(string pUserName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @NAME VARCHAR(30)");
            stringBuilder.AppendLine($"SET @NAME = '{pUserName}'");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  Name,");
            stringBuilder.AppendLine("  Login,");
            stringBuilder.AppendLine("  Code,");
            stringBuilder.AppendLine("  Money");
            stringBuilder.AppendLine("FROM USERS WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE Login = @NAME");


            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                
                return new DML.Player
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Login = row["Login"].ToString(),
                    Password = null,
                    Code = row["Code"].ToString(),
                    Money = Convert.ToDouble(row["Money"])
                };                
            }
            return null;
        }

        public DML.Player FindPlayerByCode(string code)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @CODE VARCHAR(30)");
            stringBuilder.AppendLine($"SET @CODE = '{code}'");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  Name,");
            stringBuilder.AppendLine("  Login,");
            stringBuilder.AppendLine("  Code,");
            stringBuilder.AppendLine("  Money");
            stringBuilder.AppendLine("FROM USERS WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE Code = @CODE");


            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                return new DML.Player
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Login = row["Login"].ToString(),
                    Password = null,
                    Code = row["Code"].ToString(),
                    Money = Convert.ToDouble(row["Money"])
                };
            }
            return null;
        }

        public void InsertPlayer(DML.Player playerObject)
        {
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(playerObject).ToString());
        }

        /// <summary>
        /// Check if password is match
        /// </summary>
        /// <param name="pLogin">Login</param>
        /// <param name="pPassword">Password</param>
        /// <returns></returns>
        public bool PasswordMatch(string pLogin, string pPassword)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @LOGIN VARCHAR(30),");
            stringBuilder.AppendLine("  @PASSWORD VARCHAR(30)");
            stringBuilder.AppendLine($"SET @LOGIN = '{pLogin}'");
            stringBuilder.AppendLine($"SET @PASSWORD = '{pPassword}'");
            stringBuilder.AppendLine("SELECT TOP 1 1 FROM Users WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE LOGIN = @LOGIN AND PASSWORD = @PASSWORD");

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }

        public void Update(DML.Player oldPlayer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @PASSWORD VARCHAR(30), @NAME VARCHAR(30), @ID INT");
            stringBuilder.AppendLine($"SET @PASSWORD = '{oldPlayer.Password}'");
            stringBuilder.AppendLine($"SET @NAME = '{oldPlayer.Name}'");
            stringBuilder.AppendLine($"SET @ID = {oldPlayer.ID}");
            stringBuilder.AppendLine("UPDATE USERS");
            stringBuilder.AppendLine("SET NAME = @NAME,");
            stringBuilder.AppendLine("  PASSWORD = @PASSWORD");
            stringBuilder.AppendLine("WHERE ID @ID");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }

        public DML.Player FindPlayerById(int playerId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID VARCHAR(30)");
            stringBuilder.AppendLine($"SET @ID = '{playerId}'");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  Name,");
            stringBuilder.AppendLine("  Login,");
            stringBuilder.AppendLine("  Code,");
            stringBuilder.AppendLine("  Money");
            stringBuilder.AppendLine("FROM USERS WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE ID = @ID");


            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                return new DML.Player
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Login = row["Login"].ToString(),
                    Password = null,
                    Code = row["Code"].ToString(),
                    Money = Convert.ToDouble(row["Money"])
                };
            }
            return null;
        }
    }
}