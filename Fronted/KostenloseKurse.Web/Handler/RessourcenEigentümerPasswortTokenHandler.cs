using KostenloseKurse.Web.Ausnahmen;
using KostenloseKurse.Web.Dienste.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Handler
{
    public class RessourcenEigentümerPasswortTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityDienst _identityDienst;
        private readonly ILogger<RessourcenEigentümerPasswortTokenHandler> _logger;

        public RessourcenEigentümerPasswortTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityDienst identityDienst, ILogger<RessourcenEigentümerPasswortTokenHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityDienst = identityDienst;
            _logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var antwort = await base.SendAsync(request, cancellationToken);

            if (antwort.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _identityDienst.RufenAccessTokenNachAktualisierungsTokenAuf();

                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", 
                        tokenResponse.AccessToken);

                    antwort = await base.SendAsync(request, cancellationToken);
                }
            }

            if (antwort.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new NichtAutorisierteAusnahme();
            }

            return antwort;
        }
    }
}
