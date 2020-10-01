using System.ComponentModel;

namespace BattleshipApi.Match.DML.Enumerados
{
    /// <summary>
    /// Match Status
    /// </summary>
    public enum MatchStatus
    {
        /// <summary>
        /// Not Defined
        /// </summary>
        [Description(""), DefaultValue("0")]
        NotDefined = 0,

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
