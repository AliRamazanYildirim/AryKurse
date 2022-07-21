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
        private readonly IRabattDienst _rabattDienst;

        public KorbDienst(HttpClient httpClient, IRabattDienst rabattDienst)
        {
            _httpClient = httpClient;
            _rabattDienst = rabattDienst;
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

        public async Task<bool> RabattAnwenden(string rabattCode)
        {
            await AngewandterRabattStornieren();

            var korb = await RufKorb();
            if (korb == null)
            {
                return false;
            }

            var gabRabatt = await _rabattDienst.RufRabattAuf(rabattCode);
            if (gabRabatt == null)
            {
                return false;
            }

            korb.AngewandterRabatt(gabRabatt.Code, gabRabatt.Rate);
            await SpeichernOderAktualisieren(korb);
            return true;
        }

        public async Task<bool> AngewandterRabattStornieren()
        {
            var korb = await RufKorb();
            if(korb==null || korb.RabattCode == null)
            {
                return false;
            }

            korb.RabattStornieren();
            await SpeichernOderAktualisieren(korb);
            return true;
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
