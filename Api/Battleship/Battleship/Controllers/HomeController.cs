using Microsoft.AspNetCore.Mvc;

namespace Battleship.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new ViewResult();
        }
    }
}
