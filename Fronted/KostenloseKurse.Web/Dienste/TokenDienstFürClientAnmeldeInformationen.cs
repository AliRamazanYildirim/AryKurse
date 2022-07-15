using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class TokenDienstFürClientAnmeldeInformationen : ITokenDienstFürClientAnmeldeInformationen
    {
        private readonly DienstApiEinstellungen _dienstApiEinstellungen;
        private readonly ClientEinstellungen _clientEinstellungen;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpClient;

        public TokenDienstFürClientAnmeldeInformationen(DienstApiEinstellungen dienstApiEinstellungen,IOptions 
         <ClientEinstellungen> clientEinstellungen, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _dienstApiEinstellungen = dienstApiEinstellungen;
            _clientEinstellungen = clientEinstellungen.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> BekommeToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken",default);

            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _dienstApiEinstellungen.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientEinstellungen.WebClient.ClientId,
                ClientSecret = _clientEinstellungen.WebClient.ClientSecret,
                Address = discovery.TokenEndpoint
            };

            var neueToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (neueToken.IsError)
            {
                throw neueToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", neueToken.AccessToken, neueToken.ExpiresIn,default);

            return neueToken.AccessToken;
        }
    
    }
}
