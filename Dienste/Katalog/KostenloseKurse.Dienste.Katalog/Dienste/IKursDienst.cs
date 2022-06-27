using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Modelle;
using KostenloseKurse.Shared.Düo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Dienste
{
    public interface IKursDienst
    {
        Task<Antwort<List<KursDüo>>> RufAlleDatenAsync();
        Task<Antwort<KursDüo>> RufZurIDAsync(string ID);
        Task<Antwort<List<KursDüo>>> RufZurBenutzerIDAsync(string benutzerID);
        Task<Antwort<KursDüo>> ErstellenAsync(KursErstellenDüo kursErstellenDüo);
        Task<Antwort<KeinInhaltDüo>> AktualisierenAsync(KursAktualisierenDüo kursAktualisierenDüo);
        Task<Antwort<KeinInhaltDüo>> LöschenAsync(string ID);
    }
}
