using KostenloseKurse.Dienste.Katalog.Dienste;
using KostenloseKurse.Dienste.Katalog.Einstellungen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

namespace KostenloseKurse.Dienste.Katalog
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
            services.AddScoped<IKategorieDienst, KategorieDienst>();
            services.AddScoped<IKursDienst, KursDienst>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.Configure<DatenbankEinstellungen>(Configuration.GetSection("DatenbankEinstellungen"));
            services.AddSingleton<IDatenbankEinstellungen>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatenbankEinstellungen>>().Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KostenloseKurse.Dienste.Katalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KostenloseKurse.Dienste.Katalog v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
