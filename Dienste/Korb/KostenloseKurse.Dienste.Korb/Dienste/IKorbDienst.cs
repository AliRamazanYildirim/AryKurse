using KostenloseKurse.Dienste.Korb.Düo;
using KostenloseKurse.Shared.Düo;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Korb.Dienste
{
    public interface IKorbDienst
    {
        Task<Antwort<KorbDüo>> RufKorb(string benutzerID);
        Task<Antwort<bool>> SpeichernOderAktualisieren(KorbDüo korbDüo);
        Task<Antwort<bool>> Löschen(string benutzerID);
    }
}
