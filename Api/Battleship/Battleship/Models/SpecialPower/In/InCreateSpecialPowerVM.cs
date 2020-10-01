using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.SpecialPower.In
{
    public class InCreateSpecialPowerVM
    {
        /// <summary>
        /// Name of special power
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(length: 30, ErrorMessage = "Name max length is 30 positions")]
        public string Name { get; set; }

        /// <summary>
        /// Quantifier by type
        /// </summary>
        [Required(ErrorMessage = "Quantifier is required")]
        public uint Quantifier { get; set; }

        /// <summary>
        /// how much special power costs
        /// </summary>
        [Required(ErrorMessage = "The cost of special power is required")]
        public double Cost { get; set; }

        /// <summary>
        /// Special power type
        /// </summary>
        [Required(ErrorMessage ="Special power type is required")]
        public int Type { get; set; }
    }
}
