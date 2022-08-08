using System.ComponentModel.DataAnnotations;

namespace KostenloseKurse.Web.Models.Bestellungen
{
    public class CheckoutInfoEingabe
    {
        [Display(Name = "Provinz")]
        public string Provinz { get; set; }

        [Display(Name = " Gebiet")]
        public string Gebiet { get; set; }

        [Display(Name = "Strasse")]
        public string Strasse { get; set; }

        [Display(Name = "Postleitzahl")]
        public string Postleitzahl { get; set; }

        [Display(Name = "Adresse")]
        public string Linie { get; set; }

        [Display(Name = "Kartenname Nachname")]
        public string KartenName { get; set; }

        [Display(Name = "Kartennummer")]
        public string KartenNummer { get; set; }

        [Display(Name = "Ablaufdatum (Monat/Jahr)")]
        public string Ablauf { get; set; }

        [Display(Name = "CVV/CVC2 Nummer")]
        public string CVV { get; set; }
    }
}
