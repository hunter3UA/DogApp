using Microsoft.AspNetCore.Mvc;

namespace DogApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }
    }
}
