using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
