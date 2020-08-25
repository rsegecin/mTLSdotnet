using Microsoft.AspNetCore.Mvc;
using Service.Filter;

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
        [ValidateClientCertificate]
        public IActionResult ValidateCertificate() 
        {
            return Ok("mTLS is Burning =)");
        }
    }
}
