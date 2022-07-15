using KostenloseKurse.Web.Models;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IBenutzerDienst
    {
        Task<BenutzerViewModell> RufNachBenutzerAuf();
    }
}
