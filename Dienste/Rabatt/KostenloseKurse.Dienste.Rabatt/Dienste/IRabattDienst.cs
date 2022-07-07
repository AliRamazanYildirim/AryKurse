using KostenloseKurse.Shared.Düo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Rabatt.Dienste
{
    public interface IRabattDienst
    {
        Task<Antwort<List<Modell.Rabatt>>>NehmenAlleDaten();
        Task<Antwort<Modell.Rabatt>> RufenNachIDAuf(int ID);
        Task<Antwort<KeinInhaltDüo>> Speichern(Modell.Rabatt rabatt);
        Task<Antwort<KeinInhaltDüo>> Aktualisieren(Modell.Rabatt rabatt);
        Task<Antwort<KeinInhaltDüo>> Löschen(int ID);
        Task<Antwort<Modell.Rabatt>> RufenNachCodeUndBenutzerID(string code, string benutzerID);



    }
}
