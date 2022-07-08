using KostenloseKurse.Dienste.Bestellung.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate
{
    public class BestellungsArtikel:Einheit
    {
        public string ProduktID { get; private set; }
        public string ProduktName { get; private set; }
        public string BildUrl { get; private set; }
        public Decimal Preis { get; private set; }

        //public BestellungsArtikel()
        //{
        //}

        public BestellungsArtikel(string produktID, string produktName, string bildUrl, decimal preis)
        {
            ProduktID = produktID;
            ProduktName = produktName;
            BildUrl = bildUrl;
            Preis = preis;
        }

        public void BestellungsArtikelAktualisieren(string produktName, string bildUrl, decimal preis)
        {
            ProduktName = produktName;
            Preis = preis;
            BildUrl = bildUrl;
        }
    }
}
