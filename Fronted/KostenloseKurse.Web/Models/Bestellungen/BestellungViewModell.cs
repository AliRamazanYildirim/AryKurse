using System;
using System.Collections.Generic;

namespace KostenloseKurse.Web.Models.Bestellungen
{
    public class BestellungViewModell
    {
        public int ID { get; set; }
        public DateTime ErstellungsDatum { get; set; }

        //Nicht erhalten, da das Adressfeld in meinem Zahlungsverlauf nicht benötigt wird
        //public AdresseDüo Adresse { get; set; }

        public string AbnehmerID { get; set; }
        public List<BestellungGegenstandViewModell> BestellungsArtikel { get; set; }
    }
}
