using KostenloseKurse.Web.Dienste;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Handler;
using KostenloseKurse.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KostenloseKurse.Web.Erweiterungen
{
    public static class DienstErweiterung
    {
        public static void HtttpClientDienstErstellen(this IServiceCollection services,IConfiguration Configuration)
        {
            var dienstApiEinstellungen = Configuration.GetSection("DienstApiEinstellungen").Get<DienstApiEinstellungen>();

            services.AddHttpClient<ITokenDienstFürClientAnmeldeInformationen, TokenDienstFürClientAnmeldeInformationen>();

            services.AddHttpClient<IIdentityDienst, IdentityDienst>();

            services.AddHttpClient<IKatalogDienst, KatalogDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.Katalog.Weg}");
            }).AddHttpMessageHandler<TokenHandlerFürClientAnmeldeInformationen>();

            services.AddHttpClient<IFotoBestandDienst, FotoBestandDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.FotoBestand.Weg}");
            }).AddHttpMessageHandler<TokenHandlerFürClientAnmeldeInformationen>();

            services.AddHttpClient<IBenutzerDienst, BenutzerDienst>(options =>
            {
                options.BaseAddress = new Uri(dienstApiEinstellungen.IdentityBaseUri);

            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();

            services.AddHttpClient<IKorbDienst, KorbDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.Korb.Weg}");

            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();

            services.AddHttpClient<IRabattDienst, RabattDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.Rabatt.Weg}");

            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();

            services.AddHttpClient<IZahlungDienst, ZahlungDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.FakeZahlung.Weg}");

            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();

            services.AddHttpClient<IBestellungDienst, BestellungDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.Bestellung.Weg}");

            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();
        }

    }
}
