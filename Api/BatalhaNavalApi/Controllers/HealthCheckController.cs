using Microsoft.AspNetCore.Mvc;

namespace BatalhaNavalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        // GET api/HealthCheck
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Status: OK";
        }
    }
}
