using KostenloseKurse.Web.Models.Rabatt;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IRabattDienst
    {
        Task<RabattViewModell> RufRabattAuf(string rabatttCode);
    }
}
