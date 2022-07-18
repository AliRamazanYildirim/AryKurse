using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Dienste.Interfaces;
using KostenloseKurse.Web.Models.FotoBestand;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste
{
    public class FotoBestandDienst : IFotoBestandDienst
    {
        private readonly HttpClient _httpClient;

        public FotoBestandDienst(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FotoBestandViewModell> FotoHochladen(IFormFile bild)
        {
            if (bild == null || bild.Length <= 0)
            {
                return null;
            }
            // Beispieldateiname = 2121541555.jpg
            var randomDateiname = $"{Guid.NewGuid().ToString()}{Path.GetExtension(bild.FileName)}";

            using var mstrom = new MemoryStream();

            await bild.CopyToAsync(mstrom);

            var multipartInhalt = new MultipartFormDataContent();

            multipartInhalt.Add(new ByteArrayContent(mstrom.ToArray()), "bild", randomDateiname);

            var antwort = await _httpClient.PostAsync("fotobestand", multipartInhalt);//Controllername

            if (!antwort.IsSuccessStatusCode)
            {
                return null;
            }

            var erfolgreicheAntwort = await antwort.Content.ReadFromJsonAsync<Antwort<FotoBestandViewModell>>();

            return erfolgreicheAntwort.Daten;
        }
    

        public async Task<bool> FotoLöschen(string bildUrl)
        {
            var antwort = await _httpClient.DeleteAsync($"fotobestand?bildUrl={bildUrl}");
            return antwort.IsSuccessStatusCode;
        }
    }
}
