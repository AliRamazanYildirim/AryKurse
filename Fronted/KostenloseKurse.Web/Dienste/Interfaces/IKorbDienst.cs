using KostenloseKurse.Web.Models.Korb;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IKorbDienst
    {
        Task<bool> SpeichernOderAktualisieren(KorbViewModell korbViewModell);

        Task<KorbViewModell> RufKorb();

        Task<bool> KorbLöschen();

        Task KorbGegenstandErstellen(KorbGegenstandViewModell korbGegenstandViewModell);

        Task<bool> KorbGegenstandEntfernen(string kursID);

        Task<bool> RabattAnwenden(string rabattCode);

        Task<bool> AngewandterRabattStornieren();
    }
}
