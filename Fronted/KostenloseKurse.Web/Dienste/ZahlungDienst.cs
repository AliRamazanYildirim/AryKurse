using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.FakeZahlungen;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class ZahlungDienst: IZahlungDienst
    {
        private readonly HttpClient _httpClient;

        public ZahlungDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ZahlungErhalten(ZahlungsInformationenEingabe zahlungsInformationenEingabe)
        {
            var antwort = await _httpClient.PostAsJsonAsync<ZahlungsInformationenEingabe>("fakezahlung", zahlungsInformationenEingabe);

            return antwort.IsSuccessStatusCode;
        }
    }
}
