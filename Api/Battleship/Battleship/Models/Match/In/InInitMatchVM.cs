using System.ComponentModel.DataAnnotations;

namespace BattleshipApi.Models.Match.In
{
    /// <summary>
    /// Entry object in the create match method
    /// </summary>
    public class InInitMatchVM
    {
        /// <summary>
        /// Jogador 1
        /// </summary>
        [Required(ErrorMessage ="ID do Jogador 1 é obrigatório")]
        public int Player1 { get; set; }
        
        /// <summary>
        /// Jogador 2
        /// </summary>
        [Required(ErrorMessage = "ID do Jogador 2 é obrigatório")]
        public int Player2 { get; set; }
    }
}
