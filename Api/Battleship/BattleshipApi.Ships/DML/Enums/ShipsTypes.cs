using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BattleshipApi.Ships.DML.Enums
{

    public enum ShipsTypes
    {
        [Description(""), DefaultValue("0")]
        NotDefined = 0,

        [Description("OneField"), DefaultValue("1")]
        OneField = 1,

        [Description("TwoFields"), DefaultValue("2")]
        TwoFields = 2,

        [Description("ThreeFields"), DefaultValue("3")]
        ThreeFields = 3,

        [Description("FourFields"), DefaultValue("4")]
        FourFields = 4,

        [Description("FiveFields"), DefaultValue("5")]
        FiveFields = 5

    }
}
