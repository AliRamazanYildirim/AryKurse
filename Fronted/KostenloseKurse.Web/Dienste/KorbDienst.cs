using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Korb;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class KorbDienst : IKorbDienst
    {
        private readonly HttpClient _httpClient;

        public KorbDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> KorbGegenstandEntfernen(string kursID)
        {
            var korb = await RufKorb();

            if (korb == null)

            {
                return false;
            }

            var korbGegenstandEntfernen = korb.KorbGegenstande.FirstOrDefault(x => x.KursID == kursID);

            if (korbGegenstandEntfernen == null)
            {
                return false;
            }

            var resultatLöschen = korb.KorbGegenstande.Remove(korbGegenstandEntfernen);

            if (!resultatLöschen)
            {
                return false;
            }

            if (!korb.KorbGegenstande.Any())
            {
                korb.KorbGegenstande = null;
            }

            return await SpeichernOderAktualisieren(korb);
        }

        public async Task KorbGegenstandErstellen(KorbGegenstandViewModell korbGegenstandViewModell)
        {
            var korb = await RufKorb();

            if (korb != null)
            {
                if (!korb.KorbGegenstande.Any(x => x.KursID == korbGegenstandViewModell.KursID))
                {
                    korb.KorbGegenstande.Add(korbGegenstandViewModell);
                }
            }
            else
            {
                korb = new KorbViewModell();

                korb.KorbGegenstande.Add(korbGegenstandViewModell);
            }

            await SpeichernOderAktualisieren(korb);
        }

        public async Task<bool> KorbLöschen()
        {
            var resultat = await _httpClient.DeleteAsync("korb");

            return resultat.IsSuccessStatusCode;
        }

        public Task<bool> RabattAnwenden(string rabattCode)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RabattStornieren()
        {
            throw new System.NotImplementedException();
        }

        public async Task<KorbViewModell> RufKorb()
        {
            var antwort = await _httpClient.GetAsync("korb");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }
            var korbViewModell = await antwort.Content.ReadFromJsonAsync<Antwort<KorbViewModell>>();

            return korbViewModell.Daten;
        }

        public async Task<bool> SpeichernOderAktualisieren(KorbViewModell korbViewModell)
        {
            var antwort = await _httpClient.PostAsJsonAsync<KorbViewModell>("korb", korbViewModell);

            return antwort.IsSuccessStatusCode;
        }
    }
}
