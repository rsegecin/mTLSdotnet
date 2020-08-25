using System.Net.Http;
using System.Threading.Tasks;

namespace ClientService.Services
{
    public interface IClientService
    {
        Task<HttpResponseMessage> SayHi();
    }
}
