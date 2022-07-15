using System;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface ITokenDienstFürClientAnmeldeInformationen
    {
        Task<String> BekommeToken();
    }
}
