#region Histórico de manutenção
/*
 * Nome: Pedro Henrique Pires
 * Data: 21/09/2020
 * Descrição: Implementação inicial dos status de partida
 */

#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
        [Description(""), DefaultValue("")]
        NaoDefinido,

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
