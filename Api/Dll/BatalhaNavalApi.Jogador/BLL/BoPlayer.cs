#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da interface de negócio para jogador
 */

#endregion
using BatalhaNavalApi.Jogador.DML.Interfaces;
using System;

namespace BatalhaNavalApi.Jogador.BLL
{
    /// <summary>
    /// Classe de negócio de jogador
    /// </summary>
    public class BoPlayer : IBoPlayer
    {
        /// <summary>
        /// Verifica se o jogador existe
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Se o jogador existe ou não</returns>
        public bool JogadorExiste(int pIdJogador)
        {
            if (pIdJogador <= 0)
                throw new Exception("ID do jogador inválido");

            return true;
        }

    }
}
