using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.Kataloge;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class KatalogDienst : IKatalogDienst
    {
        private readonly HttpClient _httpClient;

        public KatalogDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> KursAktualisierenAsync(KursEingabeAktualisieren kursEingabeAktualisieren)
        {
            //var resultatFotoDienst = await _photoStockService.UploadPhoto(kursEingabeErstellen.PhotoFormFile);

            //if (resultatFotoDienst != null)
            //{
            //    kursEingabeErstellen.Bild = resultatFotoDienst.Url;
            //}

            var antwort = await _httpClient.PostAsJsonAsync<KursEingabeAktualisieren>("kurse", kursEingabeAktualisieren);

            return antwort.IsSuccessStatusCode;
        }

        public async Task<bool> KursErstellenAsync(KursEingabeErstellen kursEingabeErstellen)
        {
            var antwort = await _httpClient.PostAsJsonAsync<KursEingabeErstellen>("kurse", kursEingabeErstellen);

            return antwort.IsSuccessStatusCode;
        }

        public async Task<bool> KursLöschenAsync(string kursID)
        {
            var antwort = await _httpClient.DeleteAsync($"kursID");

            return antwort.IsSuccessStatusCode;
        }

        public async Task<List<KategorieViewModell>> RufAlleKategorienAufAsync()
        {
            var antwort = await _httpClient.GetAsync("kategorien");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var antwortErfolg = await antwort.Content.ReadFromJsonAsync<Antwort<List<KategorieViewModell>>>();
            
            return antwortErfolg.Daten;
        }

        public async Task<List<KursViewModell>> RufAlleKurseAufAsync()
        {
            //http:localhost:5000/dienste/katalog/kurse
            var antwort = await _httpClient.GetAsync("kurse");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var antwortErfolg = await antwort.Content.ReadFromJsonAsync<Antwort<List<KursViewModell>>>();
            antwortErfolg.Daten.ForEach(x =>
            {
                //x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Bild);
            });
            return antwortErfolg.Daten;
        }

        public async Task<List<KursViewModell>> RufAlleKurseNachBenutzerIDAufAsync(string benutzerID)
        {
            //[controller]/ RufAlleZurBenutzerID /{ benutzerID}
            var antwort = await _httpClient.GetAsync($"kurse/RufAlleZurBenutzerID/{benutzerID}");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var antwortErfolg = await antwort.Content.ReadFromJsonAsync<Antwort<List<KursViewModell>>>();

            return antwortErfolg.Daten;
        }

        public async Task<KursViewModell> RufNachKursIDAuf(string kursID)
        {
            //[controller]/ RufAlleZurBenutzerID /{ benutzerID}
            var antwort = await _httpClient.GetAsync($"kurse/{kursID}");

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var antwortErfolg = await antwort.Content.ReadFromJsonAsync<Antwort<KursViewModell>>();

            return antwortErfolg.Daten;
        }
    }
}
