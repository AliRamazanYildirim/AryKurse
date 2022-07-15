using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class BenutzerDienst : IBenutzerDienst
    {
        private readonly HttpClient _httpClient;

        public BenutzerDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BenutzerViewModell> RufNachBenutzerAuf()
        {
            return await _httpClient.GetFromJsonAsync<BenutzerViewModell>("/api/benutzer/rufnachbenutzerauf");
        }
    }
}
