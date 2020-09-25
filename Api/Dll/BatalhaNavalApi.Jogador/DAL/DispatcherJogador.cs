#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de conexão com banco de dados para jogador
 */

#endregion
using BatalhaNavalApi.Base.DAL;
using BatalhaNavalApi.Jogador.DML.Interfaces;
using DataBaseHelper.Interfaces;
using System.Data;
using System.Text;

namespace BatalhaNavalApi.Jogador.DAL
{
    /// <summary>
    /// Classe de comunicação com o banco de dados para assunto de jogador
    /// </summary>
    public class DispatcherJogador : DispatcherBase, IDispatcherPlayer
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIUnitOfWork"></param>
        public DispatcherJogador(IUnitOfWork pIUnitOfWork) : base(pIUnitOfWork)
        {
        }

        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        public bool JogadorExiste(int pIdJogador)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("DECLARE @ID INT");
            stringBuilder.AppendLine($"SET @ID = {pIdJogador}");
            stringBuilder.AppendLine("SELECT TOP 1 1 FROM USERS WITH(NOLOCK) WHERE ID = @ID");
                    
            DataSet ds = IUnitOfWork.Consulta(stringBuilder.ToString());

            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
    }
}
