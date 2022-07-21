using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Rabatt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class RabattDienst : IRabattDienst
    {
        private readonly HttpClient _httpClient;

        public RabattDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RabattViewModell> RufRabattAuf(string rabatttCode)
        {
            //[controller]/[action]/{Code}

            var antwort = await _httpClient.GetAsync($"rabatt/RufenNachCodeAuf/{rabatttCode}");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var rabatt = await antwort.Content.ReadFromJsonAsync<Antwort<RabattViewModell>>();

            return rabatt.Daten;
        }
    }
}
