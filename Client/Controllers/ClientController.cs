using ClientService.Services;
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
                HttpResponseMessage httpResponseMessage = await clientService.SayHi();

                if (httpResponseMessage != null)
                    return Ok(httpResponseMessage.Content);
                else
                    return Ok("Didn't get any response back");
            }
            catch (Exception)
            {
                return StatusCode(501);
            }
        }
    }
}
