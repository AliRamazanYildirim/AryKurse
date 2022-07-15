using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KostenloseKurse.Web.Models.Kataloge
{
    public class KursEingabeErstellen
    {
        [Display(Name = "Kursname")]
        public string Name { get; set; }
        [Display(Name = "Kursbeschreibung")]
        public string Bezeichnung { get; set; }
        [Display(Name = "Kurspreis")]
        public decimal Preis { get; set; }
        public string BenutzerID { get; set; }
        public string Bild { get; set; }
        
        public EigenschaftViewModell Eigenschaft { get; set; }
        [Display(Name = "Kurskategorie")]
        public string KategorieID { get; set; }
        [Display(Name = "Kursbild")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
