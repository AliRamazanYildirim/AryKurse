using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Modelle;
using KostenloseKurse.Shared.Düo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Dienste
{
    public interface IKategorieDienst
    {
        Task<Antwort<List<KategorieDüo>>> RufAlleDatenAsync();
        Task<Antwort<KategorieDüo>> ErstellenAsync(Kategorie kategorie);
        Task<Antwort<KategorieDüo>> RufZurIDAsync(string ID);
    }
}
