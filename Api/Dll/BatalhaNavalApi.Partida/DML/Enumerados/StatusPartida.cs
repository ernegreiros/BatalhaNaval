#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 21/09/2020
 * Descrição: Implementação inicial dos status de partida
 */

/*
 * Nome: Pedro Henrique Pires
 * Data: 23/09/2020
 * Descrição: Atualizando default value do "NaoDefinido"
 */

#endregion
using System.ComponentModel;

namespace BatalhaNavalApi.Partida.DML.Enumerados
{
    /// <summary>
    /// Status da partida
    /// </summary>
    public enum StatusPartida
    {
        /// <summary>
        /// Não definido
        /// </summary>
        [Description(""), DefaultValue("0")]
        NaoDefinido = 0,

        /// <summary>
        /// Iniciada
        /// </summary>
        [Description("Iniciada"), DefaultValue("1")]
        Iniciada = 1,

        /// <summary>
        /// Encerrada
        /// </summary>
        [Description("Encerrada"), DefaultValue("2")]
        Encerrada = 2
    }
}
