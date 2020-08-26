using ClientServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;

namespace Client
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        X509Certificate2 clientCertificate;
        X509Certificate2 serviceCertificate;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

            clientCertificate = new X509Certificate2(
                configuration.GetValue<string>("Kestrel:Certificates:Default:Path"),
                configuration.GetValue<string>("Kestrel:Certificates:Default:Password"));

            serviceCertificate = new X509Certificate2(
                configuration.GetValue<string>("Microservices:CertPath"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddClientService(configuration, clientCertificate, serviceCertificate.Thumbprint);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
