using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Service.Filter
{
    public class ValidateClientCertificate : TypeFilterAttribute
    {
        public ValidateClientCertificate() : base(typeof(ValidateClientCertificatesImpl))
        {

        }

        private class ValidateClientCertificatesImpl : IAsyncAuthorizationFilter
        {
            X509Certificate2 clientCertificate;
            public ValidateClientCertificatesImpl()
            {
                clientCertificate = new X509Certificate2("client.crt");
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var certificate = await context.HttpContext.Connection.GetClientCertificateAsync();

                if ((certificate == null) || (!certificate.Thumbprint.Equals(clientCertificate.Thumbprint)))
                {
                    context.Result = new UnauthorizedObjectResult("");
                    return;
                }
            }
        }
    }
}
