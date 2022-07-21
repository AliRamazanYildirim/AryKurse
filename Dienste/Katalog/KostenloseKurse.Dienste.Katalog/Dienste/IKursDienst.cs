using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Modelle;
using KostenloseKurse.Shared.Düo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Dienste
{
    public interface IKursDienst
    {
        Task<Antwort<List<KursDüo>>> RufAlleDatenAufAsync();
        Task<Antwort<KursDüo>> RufNachIDAufAsync(string ID);
        Task<Antwort<List<KursDüo>>> RufNachBenutzerIDAufAsync(string benutzerID);
        Task<Antwort<KursDüo>> KursErstellenAsync(KursErstellenDüo kursErstellenDüo);
        Task<Antwort<KeinInhaltDüo>> KursAktualisierenAsync(KursAktualisierenDüo kursAktualisierenDüo);
        Task<Antwort<KeinInhaltDüo>> KursLöschenAsync(string ID);
    }
}
