#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Implementação inicial da classe de retorno base
 */

#endregion

namespace BatalhaNavalApi.Models
{
    /// <summary>
    /// Classe de retorno base
    /// </summary>
    public class OutBase
    {
        /// <summary>
        /// Status http
        /// </summary>
        public int StatusHttp { get; set; }
        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        public string Mensagem { get; set; }
    }
}
