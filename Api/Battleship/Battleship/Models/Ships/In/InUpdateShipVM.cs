using BattleshipApi.Ships.DML.Enums;
using System.ComponentModel.DataAnnotations;

namespace Battleship.Models.Ships.In
{
    public class InUpdateShipVM
    {
        [Required(ErrorMessage = "Id is Required")]
        public int ID { get; set; }
        public string Name { get; set; }
        public ShipsTypes? Type { get; set; }
        public int? ThemeId { get; set; }
        public int? ImageId { get; set; }
    }
}
