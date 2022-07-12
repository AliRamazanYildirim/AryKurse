using IdentityModel.Client;
using KostenloseKurse.Shared.Düo;
using KostenloseKurse.Web.Models;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IIdentityDienst
    {
        Task<Antwort<bool>> Einloggen(EingabeAnmelden eingabeAnmelden);

        Task<TokenResponse> RufenAccessTokenNachAktualisierungsTokenAuf();

        Task WiderrufenAktualisierungsToken();
    }
}
