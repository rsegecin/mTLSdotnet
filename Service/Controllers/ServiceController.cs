using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult HealthCheck() 
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult ValidateCertificate() 
        {
            return Ok("mTLS is Burning =)");
        }
    }
}
