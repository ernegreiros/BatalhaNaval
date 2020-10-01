using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleshipApi.SpecialPower.DML.Enums
{
    /// <summary>
    /// Special power types
    /// </summary>
    public enum SpecialPowerTypes
    {
        /// <summary>
        /// Not Defined
        /// </summary>
        [Description(""), DefaultValue("0")]
        NotDefined = 0,

        /// <summary>
        /// Attack
        /// </summary>
        [Description("Attack"), DefaultValue("1")]
        Attack = 1,

        /// <summary>
        /// Defense
        /// </summary>
        [Description("Defense"), DefaultValue("2")]
        Defense = 2,

        /// <summary>
        /// Preview
        /// </summary>
        [Description("Preview"), DefaultValue("3")]
        Preview = 3
    }
}
