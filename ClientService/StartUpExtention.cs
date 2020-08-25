using ClientService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ClientService
{
    public static class StartUpExtention
    {
        public static void AddClientService(this IServiceCollection services, IConfiguration configuration, X509Certificate2 clientCertificate, string trustThumbprint)
        {
            Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> ValidateServiceCertficate =
                delegate (object sender, X509Certificate serviceCertificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    if ((!string.IsNullOrEmpty(trustThumbprint)) && (serviceCertificate.GetCertHashString().Equals(trustThumbprint)))
                    {
                        return true;
                    }

                    return false;
                };

            services.AddHttpClient<IClientService, Services.ClientService>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(configuration.GetValue<string>("Microservices:ServiceAPI"));
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    HttpClientHandler httpClientHandler = new HttpClientHandler();

                    httpClientHandler.ServerCertificateCustomValidationCallback = ValidateServiceCertficate;
                    httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    httpClientHandler.ClientCertificates.Add(clientCertificate);

                    return httpClientHandler;
                });
        }
    }
}
