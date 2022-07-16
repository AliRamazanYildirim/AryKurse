using KostenloseKurse.Web.Ausnahmen;
using KostenloseKurse.Web.Dienste.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Handler
{
    public class TokenHandlerFürClientAnmeldeInformationen : DelegatingHandler
    {
        private readonly ITokenDienstFürClientAnmeldeInformationen _dienstFürClientAnmeldeInformationen;

        public TokenHandlerFürClientAnmeldeInformationen(ITokenDienstFürClientAnmeldeInformationen dienstFürClientAnmeldeInformationen)
        {
            _dienstFürClientAnmeldeInformationen = dienstFürClientAnmeldeInformationen;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _dienstFürClientAnmeldeInformationen.BekommeToken());

            var antwort = await base.SendAsync(request, cancellationToken);

            if (antwort.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new NichtAutorisierteAusnahme();
            }

            return antwort;
        }

    }
}
