using System.Collections.Generic;
using System.Linq;

namespace KostenloseKurse.Dienste.Korb.Düo
{
    public class KorbDüo
    {
        public string BenuzterID { get; set; }
        public string Rabattcode { get; set; }
        public List<KorbGegenstandDüo> KorbGegenstände { get; set; }
        public decimal Gesamtpreis { get => KorbGegenstände.Sum(x => x.Preis * x.Menge); }
    }
}
