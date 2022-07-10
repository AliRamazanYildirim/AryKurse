using KostenloseKurse.Dienste.Katalog.Dienste;
using KostenloseKurse.Dienste.Katalog.Düo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dienstAnbieter = scope.ServiceProvider;

                var kategorieDienst = dienstAnbieter.GetRequiredService<IKategorieDienst>();

                if (!kategorieDienst.RufAlleDatenAsync().Result.Daten.Any())
                {
                    kategorieDienst.ErstellenAsync(new KategorieDüo { Name = "Asp.net Core Kurs" }).Wait();
                    kategorieDienst.ErstellenAsync(new KategorieDüo { Name = "Asp.net Core API Kurs" }).Wait();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
