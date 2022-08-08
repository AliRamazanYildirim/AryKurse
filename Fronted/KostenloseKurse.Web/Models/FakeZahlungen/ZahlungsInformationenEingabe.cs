using KostenloseKurse.Web.Models.Bestellungen;

namespace KostenloseKurse.Web.Models.FakeZahlungen
{
    public class ZahlungsInformationenEingabe
    {
        public string KartenName { get; set; }
        public string KartenNummer { get; set; }
        public string Ablauf { get; set; }
        public string CVV { get; set; }
        public decimal GesamtPreis { get; set; }

        public BestellungErstellungEingabe Bestellung { get; set; }
    }
}
