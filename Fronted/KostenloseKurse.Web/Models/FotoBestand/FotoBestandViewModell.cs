using System.ComponentModel.DataAnnotations;

namespace KostenloseKurse.Web.Models.FotoBestand
{
    public class FotoBestandViewModell
    {
        [Display(Name = "Bildurl")]
        public string Url { get; set; }
    }
}
