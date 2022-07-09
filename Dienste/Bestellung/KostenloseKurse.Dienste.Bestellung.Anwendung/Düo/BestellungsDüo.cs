using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Düo
{
    public class BestellungsDüo
    {
        public int ID { get; set; }
        public DateTime ErstellungsDatum { get;  set; }

        public AdresseDüo Adresse { get;  set; }

        public string AbnehmerID { get;  set; }
        public List<BestellungsArtikelDüo> BestellungsArtikel { get; set; }
    }
}
