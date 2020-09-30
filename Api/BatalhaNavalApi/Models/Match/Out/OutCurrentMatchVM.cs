using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatalhaNavalApi.Models.Match.Out
{

    /// <summary>
    /// Class that returns the current match
    /// </summary>
    public class OutCurrentMatchVM : OutBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Player 1 ID
        /// </summary>
        public int Player1 { get; set; }
        /// <summary>
        /// Player 2 ID
        /// </summary>
        public int Player2 { get; set; }
        /// <summary>
        /// Match Status
        /// </summary>
        public string MatchStatus { get; set; }
    }
}
