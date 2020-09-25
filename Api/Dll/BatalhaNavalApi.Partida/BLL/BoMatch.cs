#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 21/09/2020
 * Descrição: Implementação inicial da classe de negócio de partida
 */

/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial das regras de negócio da interface herdada
 */

/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Ajustando para setar a partida como iniciada antes de gravar
 */

#endregion

using BatalhaNavalApi.Jogador.DML;
using BatalhaNavalApi.Jogador.DML.Interfaces;
using BatalhaNavalApi.Partida.DML.Interfaces;
using System;

namespace BatalhaNavalApi.Partida.BLL
{
    /// <summary>
    /// Classe de negócio para partida
    /// </summary>
    public class BoMatch : IBoMatch
    {
        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pIBoJogador">Classe de negócio de jogador</param>
        /// <param name="pIDispatcherPartida">Classe de conexão com o banco de dados para partida</param>
        public BoMatch(IBoPlayer pIBoJogador, IDispatcherMatch pIDispatcherPartida)
        {
            IBoJogador = pIBoJogador;
            IDispatcherPartida = pIDispatcherPartida;
        }

        #endregion

        #region Propriedades readonly
        /// <summary>
        /// Classe de negócio de jogador
        /// </summary>
        private readonly IBoPlayer IBoJogador;

        /// <summary>
        /// Classe de conexão com o banco para partida
        /// </summary>
        private readonly IDispatcherMatch IDispatcherPartida;
        #endregion

        #region Métodos

        /// <summary>
        /// Inicia a partida e retorna o número dela
        /// </summary>
        /// <param name="pPartida">Objeto de partida</param>
        /// <returns>Número da partida</returns>
        public int IniciarPartida(DML.Match pPartida)
        {
            if (BuscaPartidaAtual(pPartida.Jogador1) != null)
                throw new Exception("Jogador 1 já possui uma partida iniciada");
            if (BuscaPartidaAtual(pPartida.Jogador2) != null)
                throw new Exception("Jogador 2 já possui uma partida iniciada");

            pPartida.StatusDaPartida = DML.Enumerados.MatchStatus.Iniciada;
            return IDispatcherPartida.BuscaPartidaAtual(pPartida.Jogador1).ID;
        }

        /// <summary>
        /// Busca a partida atual do jogador (Com status iniciada)
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Partida atual</returns>
        public DML.Match BuscaPartidaAtual(int pIdJogador)
        {
            if (IBoJogador.JogadorExiste(pIdJogador))
            {
                return IDispatcherPartida.BuscaPartidaAtual(pIdJogador);
            }
            else
                throw new Exception("Jogador informado não cadastrado");
        }

        /// <summary>
        /// Finaliza a partida
        /// </summary>
        /// <param name="pIdPartida">Id da partida</param>
        public void FinalizarPartida(int pIdPartida)
        {
            if (pIdPartida <= 0)
                throw new Exception("Id da partida é obrigatório");

            IDispatcherPartida.FinalizarPartida(pIdPartida);
        }
        #endregion


    }
}
