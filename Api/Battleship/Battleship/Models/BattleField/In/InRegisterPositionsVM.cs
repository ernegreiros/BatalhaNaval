using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.BattleField.In
{
    public class InRegisterPositionsVM
    {
        /// <summary>
        /// Player
        /// </summary>
        [Required(ErrorMessage ="Player is required")]
        public int Player { get; set; }

        /// <summary>
        /// Position X
        /// </summary>
        [Required(ErrorMessage ="Position X is required")]
        public int PosX { get; set; }

        /// <summary>
        /// Position Y
        /// </summary>
        [Required(ErrorMessage = "Position Y is required")]
        public int PosY { get; set; }

        /// <summary>
        /// Match ID
        /// </summary>
        [Required(ErrorMessage = "Match ID is required")]
        public int MatchID { get; set; }
    }
}
