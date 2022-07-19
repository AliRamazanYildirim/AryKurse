using System;

namespace KostenloseKurse.Web.Models.Kataloge
{
    public class KursViewModell
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }
        public string KurzeBezeichnung 
        { 
            get => Bezeichnung.Length > 100 ? Bezeichnung.Substring(0, 100) + "..." : Bezeichnung;
        }
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Bild { get; set; }
        public string FotoBestandUrl { get; set; }
        public DateTime Erstellungsdatum { get; set; }
        public EigenschaftViewModell Eigenschaft { get; set; }
        public string KategorieID { get; set; }
        public KategorieViewModell Kategorie { get; set; }
    }
}
