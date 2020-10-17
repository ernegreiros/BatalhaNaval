using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Themes.In
{
    public class InPostThemesVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name max length is 30 caracters")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Description max length is 100 caracters")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [MaxLength(200, ErrorMessage = "Image path max length is 200 caracters")]
        [Required(ErrorMessage = "Image path is required")]
        public string ImagePath { get; set; }
    }
}
