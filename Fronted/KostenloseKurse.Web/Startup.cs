using KostenloseKurse.Shared.Dienste;
using KostenloseKurse.Web.Dienste;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Handler;
using KostenloseKurse.Web.Helfer;
using KostenloseKurse.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DienstApiEinstellungen>(Configuration.GetSection("DienstApiEinstellungen"));
            services.Configure<ClientEinstellungen>(Configuration.GetSection("ClientEinstellungen"));
            services.AddHttpContextAccessor();
            services.AddAccessTokenManagement();
            services.AddSingleton<FotoHelfer>();
            services.AddScoped<ISharedIdentityDienst, SharedIdentityDienst>();
            var dienstApiEinstellungen = Configuration.GetSection("DienstApiEinstellungen").Get<DienstApiEinstellungen>();
            services.AddHttpClient<ITokenDienstFürClientAnmeldeInformationen, TokenDienstFürClientAnmeldeInformationen>();

            services.AddScoped<RessourcenEigentümerPasswortTokenHandler>();
            services.AddScoped<TokenHandlerFürClientAnmeldeInformationen>();
            services.AddHttpClient<IIdentityDienst, IdentityDienst>();

            services.AddHttpClient<IKatalogDienst, KatalogDienst>(options=>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.Katalog.Weg}");
            }).AddHttpMessageHandler<TokenHandlerFürClientAnmeldeInformationen>();

            services.AddHttpClient<IFotoBestandDienst, FotoBestandDienst>(options =>
            {
                options.BaseAddress = new Uri($"{dienstApiEinstellungen.GatewayBaseUri}/{dienstApiEinstellungen.FotoBestand.Weg}");
            }).AddHttpMessageHandler<TokenHandlerFürClientAnmeldeInformationen>();

            services.AddHttpClient<IBenutzerDienst, BenutzerDienst>(options=>
            {
                options.BaseAddress = new Uri(dienstApiEinstellungen.IdentityBaseUri);
                
            }).AddHttpMessageHandler<RessourcenEigentümerPasswortTokenHandler>();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (CookieAuthenticationDefaults.AuthenticationScheme,options=>
                {
                    options.LoginPath = "/Auth/Einloggen";
                    options.ExpireTimeSpan = TimeSpan.FromDays(60);
                    options.SlidingExpiration = true;
                    options.Cookie.Name = "arywebcookie";
                });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/StartSeite/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=StartSeite}/{action=Index}/{id?}");
            });
        }
    }
}
