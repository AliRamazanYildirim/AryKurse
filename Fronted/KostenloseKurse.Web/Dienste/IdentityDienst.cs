using IdentityModel.Client;
using KostenloseKurse.IdentityServer.Düo;
using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class IdentityDienst : IIdentityDienst
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientEinstellungen _clientEinstellungen;
        private readonly DienstApiEinstellungen _dienstApiEinstellungen;

        public IdentityDienst(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientEinstellungen>
            clientEinstellungen,IOptions<DienstApiEinstellungen> dienstApiEinstellungen)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientEinstellungen = clientEinstellungen.Value;
            _dienstApiEinstellungen = dienstApiEinstellungen.Value;
        }

        public async Task<Antwort<bool>> Einloggen(EingabeAnmelden eingabeAnmelden)
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address=_dienstApiEinstellungen.IdentityBaseUri,
                Policy=new DiscoveryPolicy { RequireHttps=false}
            });

            if(discovery.IsError)
            {
                throw discovery.Exception;
            }
            var passwordTokenAbfrage = new PasswordTokenRequest
            {
                ClientId = _clientEinstellungen.WebClientFürBenutzer.ClientId,
                ClientSecret=_clientEinstellungen.WebClientFürBenutzer.ClientSecret,
                UserName= eingabeAnmelden.Email,
                Password= eingabeAnmelden.Passwort,
                Address=discovery.TokenEndpoint

            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenAbfrage);
            if(token.IsError)
            {
                var antwortInhalt = await token.HttpResponse.Content.ReadAsStringAsync();
                var fehlerDüo = JsonSerializer.Deserialize<FehlerDüo>(antwortInhalt, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return Antwort<bool>.Fehlschlag(fehlerDüo.Fehler, 400);
            }
            var benutzerInfoAbfrage = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discovery.UserInfoEndpoint
            };
            var benutzerInfo = await _httpClient.GetUserInfoAsync(benutzerInfoAbfrage);
            if(benutzerInfo.IsError)
            {
                throw benutzerInfo.Exception;
            }
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(benutzerInfo.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            
            ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal(claimsIdentity);
            var authentifizierungsEigenschaften = new AuthenticationProperties();

            authentifizierungsEigenschaften.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.Now.AddSeconds
                (token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });
            authentifizierungsEigenschaften.IsPersistent = eingabeAnmelden.ErinnereMich;

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authentifizierungsEigenschaften);

            return Antwort<bool>.Erfolg(200);
        }

        public async Task<TokenResponse> RufenAccessTokenNachAktualisierungsTokenAuf()
        {

            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _dienstApiEinstellungen.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }

            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = _clientEinstellungen.WebClientFürBenutzer.ClientId,
                ClientSecret = _clientEinstellungen.WebClientFürBenutzer.ClientSecret,
                RefreshToken = refreshToken,
                Address = discovery.TokenEndpoint
            };

            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                return null;
            }

            var authentifizierungsToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            };

            var authentifizierungsResultat = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var eigenschaften = authentifizierungsResultat.Properties;
            eigenschaften.StoreTokens(authentifizierungsToken);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authentifizierungsResultat.Principal, eigenschaften);

            return token;
        }

        public async Task WiderrufenAktualisierungsToken()
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _dienstApiEinstellungen.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw discovery.Exception;
            }
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            TokenRevocationRequest tokenRevocationRequest = new()
            {
                ClientId = _clientEinstellungen.WebClientFürBenutzer.ClientId,
                ClientSecret = _clientEinstellungen.WebClientFürBenutzer.ClientSecret,
                Address = discovery.RevocationEndpoint,
                Token = refreshToken,
                TokenTypeHint = "refresh_token"
            };

            await _httpClient.RevokeTokenAsync(tokenRevocationRequest);
        }
    }
}
