using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(o =>
                    {
                        o.ConfigureHttpsDefaults(o => {
                            o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                            o.ClientCertificateValidation = ValidateClientCertficate;
                        });
                    });
                });

        public static Func<X509Certificate, X509Chain, SslPolicyErrors, bool> ValidateClientCertficate =
            delegate (X509Certificate serviceCertificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                X509Certificate2 clientCertificate;
                clientCertificate = new X509Certificate2("client.crt");

                if (serviceCertificate.GetCertHashString().Equals(clientCertificate.Thumbprint))
                {
                    return true;
                }

                return false;
            };
    }
}
