﻿#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de negócio de partida
 */

#endregion

namespace BatalhaNavalApi.Partida.DML.Interfaces
{
    /// <summary>
    /// Interface da classe de negócio de partida
    /// </summary>
    public interface IBoPartida
    {
        /// <summary>
        /// Inicia a partida e retorna o número dela
        /// </summary>
        /// <param name="pPartida">Objeto de partida</param>
        /// <returns>Número da partida</returns>
        int IniciarPartida(DML.Partida pPartida);

        /// <summary>
        /// Busca a partida atual do jogador (Com status iniciada)
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Partida atual</returns>
        DML.Partida BuscaPartidaAtual(int pIdJogador);

        /// <summary>
        /// Finaliza a partida
        /// </summary>
        /// <param name="pIdPartida">Id da partida</param>
        void FinalizarPartida(int pIdPartida);
    }
}
