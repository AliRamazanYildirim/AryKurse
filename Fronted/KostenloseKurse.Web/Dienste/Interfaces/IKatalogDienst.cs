using KostenloseKurse.Web.Models.Kataloge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Web.Dienste.Interfaces
{
    public interface IKatalogDienst
    {
        Task<List<KursViewModell>> RufAlleKurseAufAsync();

        Task<List<KategorieViewModell>> RufAlleKategorienAufAsync();

        Task<List<KursViewModell>> RufAlleKurseNachBenutzerIDAufAsync(string benutzerID);

        Task<KursViewModell> RufNachKursIDAuf(string kursID);

        Task<bool> KursErstellenAsync(KursEingabeErstellen kursEingabeErstellen);

        Task<bool> KursAktualisierenAsync(KursEingabeAktualisieren kursEingabeAktualisieren);

        Task<bool> KursLöschenAsync(string kursID);
    }
}
