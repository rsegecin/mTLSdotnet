using System.Net.Http;
using System.Threading.Tasks;

namespace ClientServices.Services
{
    public interface IClientService
    {
        Task<string> SayHi();
    }
}
