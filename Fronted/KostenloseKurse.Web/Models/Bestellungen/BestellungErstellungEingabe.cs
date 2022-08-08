using System.Collections.Generic;

namespace KostenloseKurse.Web.Models.Bestellungen
{
    public class BestellungErstellungEingabe
    {
        public BestellungErstellungEingabe()
        {
            BestellungsArtikel = new List<BestellungGegenstandErstellungEingabe>();
        }
        public string AbnehmerID { get; set; }

        public List<BestellungGegenstandErstellungEingabe> BestellungsArtikel { get; set; }

        public AdresseErstellungEingabe Addresse { get; set; }
    }
}
