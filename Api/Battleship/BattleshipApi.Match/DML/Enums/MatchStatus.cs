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
        /// Waiting the battlefield positions
        /// </summary>
        [Description("Waiting the battlefield positions"), DefaultValue("1")]
        WaitingBattleField = 1,

        /// <summary>
        /// Created
        /// </summary>
        [Description("Created"), DefaultValue("2")]
        Created = 2,

        /// <summary>
        /// Closed
        /// </summary>
        [Description("Closed"), DefaultValue("3")]
        Closed = 3
    }
}
