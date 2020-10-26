using System.ComponentModel.DataAnnotations;

namespace Battleship.Models.Ships.In
{
    public class InCreateShipVM
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(length: 30, ErrorMessage = "Name max length is 30 positions")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Ship type is required")]
        public int Type { get; set; }

        [Required(ErrorMessage = "ImagePath is required")]
        public string ImagePath { get; set; }
        
        [Required(ErrorMessage = "ThemeId is required")]
        public int ThemeId { get; set; }
    }
}
