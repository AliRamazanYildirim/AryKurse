using System;

namespace KostenloseKurse.Web.Models.Kataloge
{
    public class KursViewModell
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Bild { get; set; }
        public DateTime Erstellungsdatum { get; set; }
        public EigenschaftViewModell Eigenschaft { get; set; }
        public string KategorieID { get; set; }
        public KategorieViewModell Kategorie { get; set; }
    }
}
