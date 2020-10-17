using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatalhaNavalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        // GET api/HealthCheck
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Status: OK";
        }
    }
}
