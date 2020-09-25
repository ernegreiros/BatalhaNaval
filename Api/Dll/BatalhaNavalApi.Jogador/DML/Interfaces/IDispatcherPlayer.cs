#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da interface de banco de dados para jogador
 */

#endregion

namespace BatalhaNavalApi.Jogador.DML.Interfaces
{
    /// <summary>
    /// Interface de conexão com banco de dados para jogador
    /// </summary>
    public interface IDispatcherPlayer
    {
        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        bool JogadorExiste(int pIdJogador);
    }
}
