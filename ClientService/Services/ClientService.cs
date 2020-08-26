using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientServices.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient httpClient;

        public ClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> SayHi()
        {
            Uri uri = new Uri(httpClient.BaseAddress, "Service");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent("Hi from client", Encoding.UTF8);

            using var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            content = Regex.Unescape(content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return content;
            else
                throw new Exception("Service didn't respond as expected. Status code: " + response.StatusCode);
        }
    }
}
