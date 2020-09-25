#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da interface de negócio para jogador
 */

#endregion

namespace BatalhaNavalApi.Jogador.DML.Interfaces
{
    /// <summary>
    /// Interface da classe de negócio de jogador
    /// </summary>
    public interface IBoPlayer
    {
        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        bool JogadorExiste(int pIdJogador);
    }
}
