#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de jogador
 */

#endregion
namespace BatalhaNavalApi.Jogador.DML
{
    /// <summary>
    /// Objeto de jogador
    /// </summary>
    public class Jogador
    {
        /// <summary>
        /// ID do jogador
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do jogador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
    }
}
