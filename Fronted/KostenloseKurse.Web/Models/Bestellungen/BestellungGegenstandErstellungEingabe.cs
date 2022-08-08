﻿using System;

namespace KostenloseKurse.Web.Models.Bestellungen
{
    public class BestellungGegenstandErstellungEingabe
    {
        public string ProduktID { get; set; }
        public string ProduktName { get; set; }
        public string BildUrl { get; set; }
        public Decimal Preis { get; set; }
    }
}