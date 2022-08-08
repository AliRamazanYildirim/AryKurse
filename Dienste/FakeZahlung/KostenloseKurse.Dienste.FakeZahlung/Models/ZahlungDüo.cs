namespace KostenloseKurse.Dienste.FakeZahlung.Models
{
    public class ZahlungDüo
    {
        public string KartenName { get; set; }
        public string KartenNummer { get; set; }
        public string Ablauf { get; set; }
        public string CVV { get; set; }
        public decimal GesamtPreis { get; set; }

        public BestellungDüo Bestellung { get; set; }
    }
}
