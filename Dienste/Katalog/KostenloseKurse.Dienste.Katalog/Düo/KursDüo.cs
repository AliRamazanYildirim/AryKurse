using System;

namespace KostenloseKurse.Dienste.Katalog.Düo
{
    public class KursDüo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Bezeichnung { get; set; }      
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Picture { get; set; }      
        public DateTime Erstellungsdatum { get; set; }
        public EigenschaftDüo Eigenschaft { get; set; }       
        public string KategorieID { get; set; }   
        public KategorieDüo Kategorie { get; set; }
    }
}
