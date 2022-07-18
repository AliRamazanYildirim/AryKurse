using KostenloseKurse.Web.Models.FotoBestand;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IFotoBestandDienst
    {
        Task<FotoBestandViewModell> FotoHochladen(IFormFile bild);

        Task<bool> FotoLöschen(string bildUrl);
    }
}
