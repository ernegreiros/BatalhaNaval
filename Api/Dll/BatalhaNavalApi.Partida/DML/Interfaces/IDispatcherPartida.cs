namespace BatalhaNavalApi.Partida.DML.Interfaces
{
    /// <summary>
    /// Interface da classe de conexão com o banco para assuntos de partida
    /// </summary>
    public interface IDispatcherPartida
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
