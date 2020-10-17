using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models.Auth.In
{
    public class InAuthorizeVM
    {
        [Required(ErrorMessage ="Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
