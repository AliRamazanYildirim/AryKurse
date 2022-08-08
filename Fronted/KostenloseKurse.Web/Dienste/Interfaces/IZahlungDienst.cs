using KostenloseKurse.Web.Models.FakeZahlungen;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IZahlungDienst
    {
        Task<bool> ZahlungErhalten(ZahlungsInformationenEingabe zahlungsInformationenEingabe);
    }
}
