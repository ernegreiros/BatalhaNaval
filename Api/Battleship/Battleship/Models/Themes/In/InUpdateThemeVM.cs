using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Themes.In
{
    public class InUpdateThemeVM : InPostThemesVM
    {
        [Required(ErrorMessage ="ID is required")]
        public uint? Id { get; set; }
    }
}
