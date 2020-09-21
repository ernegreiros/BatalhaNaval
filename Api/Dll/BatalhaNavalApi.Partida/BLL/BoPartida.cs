#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 21/09/2020
 * Descrição: Implementação inicial da classe de negócio de partida
 */

#endregion

namespace BatalhaNavalApi.Partida.BLL
{
    /// <summary>
    /// Classe de negócio para partida
    /// </summary>
    public class BoPartida
    {
        /// <summary>
        /// Inicia a partida e retorna o número dela
        /// </summary>
        /// <param name="pPartida">Objeto de partida</param>
        /// <returns>Número da partida</returns>
        public int IniciarPartida(DML.Partida pPartida)
        {
            /*Inicia a partida*/
            /*Verificar se algum dos jogadores já possui alguma partida em aberto*/
            /*Inserir no banco*/
            return 0;
        }

        /// <summary>
        /// Busca a partida atual do jogador (Com status iniciada)
        /// </summary>
        /// <param name="pIdJogador">Id do jogador</param>
        /// <returns>Partida atual</returns>
        public DML.Partida BuscaPartidaAtual(int pIdJogador) {
            /*Buscar no banco*/
            return new DML.Partida();
        }

        /// <summary>
        /// Finaliza a partida
        /// </summary>
        /// <param name="pIdPartida">Id da partida</param>
        public void FinalizarPartida(int pIdPartida)
        {
            /*Update no banco*/
        }

    }
}
