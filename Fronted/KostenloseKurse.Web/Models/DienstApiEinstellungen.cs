namespace KostenloseKurse.Web.Models
{
    public class DienstApiEinstellungen
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string FotoBestandUri { get; set; }

        public DienstApi Katalog { get; set; }

        public DienstApi FotoBestand { get; set; }

        public DienstApi Korb { get; set; }

        public DienstApi Rabatt { get; set; }

        public DienstApi FakeZahlung { get; set; }
        public DienstApi Bestellung { get; set; }
    }
    public class DienstApi
    {
        public string Weg { get; set; }
    }
}
