using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.SpecialPower.In
{
    public class InUpdateSpecialPowerVM
    {
        [Required(ErrorMessage = "Id is Required")]
        public int ID { get; set; }
        public string Name { get; set; }
        public uint? Quantifier { get; set; }
        public double? Cost { get; set; }
        public int? Type { get; set; }
        public double? Compensation { get; set; }
    }
}
