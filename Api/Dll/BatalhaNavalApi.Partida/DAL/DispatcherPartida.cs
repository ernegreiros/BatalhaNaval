#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de conexão com banco de dados para partida
 */

#endregion
using BatalhaNavalApi.Base.DAL;
using BatalhaNavalApi.Partida.DML.Enumerados;
using BatalhaNavalApi.Partida.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BatalhaNavalApi.Partida.DAL
{
    /// <summary>
    /// Classe de conexão com o banco de dados para partida
    /// </summary>
    public class DispatcherPartida : DispatcherBase, IDispatcherPartida
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIUnitOfWork">Objeto de conexão</param>
        public DispatcherPartida(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        #region Métodos
        /// <summary>
        /// Inicia a partida e retorna o número dela
        /// </summary>
        /// <param name="pPartida">Objeto de partida</param>
        /// <returns>Número da partida</returns>
        public int IniciarPartida(DML.Partida pPartida)
        {
            /*Inserindo no banco*/
            IUnitOfWork.Executar(IUnitOfWork.MontaInsertPorAttributo(pPartida).ToString());
            /*Retornando ID da partida*/
            return BuscaPartidaAtual(pPartida.Jogador1).ID;
        }

        /// <summary>
        /// Busca a partida atual do jogador (Com status iniciada)
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Partida atual</returns>
        public DML.Partida BuscaPartidaAtual(int pIdJogador)
        {
            #region Query
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID_JOGADOR INT");
            stringBuilder.AppendLine($"SET @ID_JOGADOR = {pIdJogador}");
            stringBuilder.AppendLine("SELECT");
            stringBuilder.AppendLine("  ID,");
            stringBuilder.AppendLine("  PLAYER1,");
            stringBuilder.AppendLine("  PLAYER2,");
            stringBuilder.AppendLine("  STATUS");
            stringBuilder.AppendLine("FROM MATCH WITH(NOLOCK)");
            stringBuilder.AppendLine("WHERE PLAYER1 = @ID_JOGADOR OR PLAYER2 = @ID_JOGADOR");
            #endregion

            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());
            DML.Partida partida = new DML.Partida();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != DBNull.Value)
                    partida.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);

                if (ds.Tables[0].Rows[0]["PLAYER1"] != DBNull.Value)
                    partida.Jogador1 = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER1"]);

                if (ds.Tables[0].Rows[0]["PLAYER2"] != DBNull.Value)
                    partida.Jogador2 = Convert.ToInt32(ds.Tables[0].Rows[0]["PLAYER2"]);

                if (ds.Tables[0].Rows[0]["STATUS"] != DBNull.Value)
                    partida.StatusDaPartida = (StatusPartida)Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"]);

                return partida;
            }
            return null;                       
        }

        /// <summary>
        /// Finaliza a partida
        /// </summary>
        /// <param name="pIdPartida">Id da partida</param>
        public void FinalizarPartida(int pIdPartida)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID");
            stringBuilder.AppendLine($"SET @ID = {pIdPartida}");
            stringBuilder.AppendLine($"UPDATE TABLE MATCH SET STATUS = {Convert.ToInt32(StatusPartida.Encerrada)} WHERE ID = @ID");

            IUnitOfWork.Executar(stringBuilder.ToString());
        }
        #endregion
    }
}
