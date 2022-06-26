namespace KostenloseKurse.Dienste.Katalog.Düo
{
    public class KursAktualisierenDüo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Picture { get; set; }
        public EigenschaftDüo Eigenschaft { get; set; }
        public string KategorieID { get; set; }
    }
}
