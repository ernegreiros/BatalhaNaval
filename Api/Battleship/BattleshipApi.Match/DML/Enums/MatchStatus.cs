using System.ComponentModel;

namespace BattleshipApi.Match.DML.Enumerados
{
    /// <summary>
    /// Status da partida
    /// </summary>
    public enum MatchStatus
    {
        /// <summary>
        /// Não definido
        /// </summary>
        [Description(""), DefaultValue("0")]
        NaoDefinido = 0,

        /// <summary>
        /// Created
        /// </summary>
        [Description("Created"), DefaultValue("1")]
        Created = 1,

        /// <summary>
        /// Closed
        /// </summary>
        [Description("Closed"), DefaultValue("2")]
        Closed = 2
    }
}
