using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("/api/ping")]
    [Route("/")]
    [ApiController]
    public class PingController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<string> Ping()
        {
            return "Pong"; 
        }
    }
}
