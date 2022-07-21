using System.Collections.Generic;
using System.Linq;

namespace KostenloseKurse.Dienste.Korb.Düo
{
    public class KorbDüo
    {
        public string BenuzterID { get; set; }
        public string Rabattcode { get; set; }
        public List<KorbGegenstandDüo> KorbGegenstande { get; set; }
        public decimal Gesamtpreis { get => KorbGegenstande.Sum(x => x.Preis * x.Menge); }
    }
}
