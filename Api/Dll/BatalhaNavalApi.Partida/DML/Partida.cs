#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 21/09/2020
 * Descrição: Implementação inicial da classe de partida
 */
#endregion
using BatalhaNavalApi.Partida.DML.Enumerados;

namespace BatalhaNavalApi.Partida.DML
{
    /// <summary>
    /// Objeto de partida
    /// </summary>
    public class Partida
    {

        /// <summary>
        /// Id da partida
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Jogador 1
        /// </summary>
        public int Jogador1 { get; set; }

        /// <summary>
        /// Jogador 2
        /// </summary>
        public int Jogador2 { get; set; }
        /// <summary>
        /// Status da partida
        /// </summary>
        public StatusPartida StatusDaPartida { get; set; }
    }
}
