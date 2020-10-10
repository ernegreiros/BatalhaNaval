using Battleship;
using System.ComponentModel.DataAnnotations;

namespace BattleshipApi.Models.Player.In
{

    public class InCreatePlayerVM 
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Code { get; set; } = Helper.KeyGenerator();

        public double Money { get; set; } = 1000.00;
    }
}
