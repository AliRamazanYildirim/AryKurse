using KostenloseKurse.Dienste.Korb.Düo;
using KostenloseKurse.Shared.Düo;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Korb.Dienste
{
    public class KorbDienst : IKorbDienst
    {
        private readonly RedisDienst _redisDienst;

        public KorbDienst(RedisDienst redisDienst)
        {
            _redisDienst = redisDienst;
        }

        public async Task<Antwort<bool>> Löschen(string benutzerID)
        {
            var status = await _redisDienst.RufDB().KeyDeleteAsync(benutzerID);
            return status ? Antwort<bool>.Erfolg(204) : Antwort<bool>.Fehlschlag("Der Korb wurde nicht gefunden!", 404);
        }

        public async Task<Antwort<KorbDüo>> RufKorb(string benutzerID)
        {
            var existierteKorb = await _redisDienst.RufDB().StringGetAsync(benutzerID);
            if(String.IsNullOrEmpty(existierteKorb))
            {
                return Antwort<KorbDüo>.Fehlschlag("Korb wurde nicht gefunden!", 404);

            }
            return Antwort<KorbDüo>.Erfolg(JsonSerializer.Deserialize<KorbDüo>(existierteKorb), 200);

        }

        public async Task<Antwort<bool>> SpeichernOderAktualisieren(KorbDüo korbDüo)
        {
            var status = await _redisDienst.RufDB().StringSetAsync(korbDüo.BenuzterID, JsonSerializer.Serialize(korbDüo));
            return status ? Antwort<bool>.Erfolg(204) : Antwort<bool>.Fehlschlag("Der Korb wurde nicht gespeichert oder aktualisiert", 500);
        }
    }
}
