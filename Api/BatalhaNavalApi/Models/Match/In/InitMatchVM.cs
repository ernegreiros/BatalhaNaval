#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de entrada para iniciar a partida
 */

#endregion
using System.ComponentModel.DataAnnotations;

namespace BatalhaNavalApi.Models.Partida.Entrada
{
    /// <summary>
    /// Objeto de entrada no método de iniciar partida
    /// </summary>
    public class InitMatchVM
    {
        /// <summary>
        /// Jogador 1
        /// </summary>
        [Required(ErrorMessage ="ID do Jogador 1 é obrigatório")]
        public int Jogador1 { get; set; }
        
        /// <summary>
        /// Jogador 2
        /// </summary>
        [Required(ErrorMessage = "ID do Jogador 2 é obrigatório")]
        public int Jogador2 { get; set; }
    }
}
