using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Themes.In
{
    public class InUpdateThemeVM
    {
        [Required(ErrorMessage ="ID is required")]
        public uint? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
