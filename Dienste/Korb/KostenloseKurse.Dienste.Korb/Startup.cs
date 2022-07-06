using KostenloseKurse.Dienste.Korb.Dienste;
using KostenloseKurse.Dienste.Korb.Einstellungen;
using KostenloseKurse.Shared.Dienste;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Korb
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
            var autorisierungsRichtlinieErfordern = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Configuration["IdentityServerUrl"];
                options.Audience = "ressource_katalog";
                options.RequireHttpsMetadata = false;//Hier erklären wir, dass wir nicht mit Https arbeiten werden.

            });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityDienst, SharedIdentityDienst>();
            services.AddScoped<IKorbDienst, KorbDienst>();
            services.Configure<RedisEinstellungen>(Configuration.GetSection("RedisEinstellungen"));


            services.AddSingleton<RedisDienst>(sp =>
            {
                var redisEinstellungen = sp.GetRequiredService < IOptions<RedisEinstellungen>>().Value;
                var redis = new RedisDienst(redisEinstellungen.Host, redisEinstellungen.Port);
                redis.Verbinden();
                return redis;
            });

            services.AddControllers(options=>
            {
                options.Filters.Add(new AuthorizeFilter(autorisierungsRichtlinieErfordern));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KostenloseKurse.Dienste.Korb", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KostenloseKurse.Dienste.Korb v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
