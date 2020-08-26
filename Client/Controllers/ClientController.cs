using ClientServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> CallService() 
        {
            try
            {
                string httpResponseMessage = await clientService.SayHi();

                if (!string.IsNullOrEmpty(httpResponseMessage))
                    return Ok(httpResponseMessage);
                else
                    return Ok("Didn't get any response back");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
