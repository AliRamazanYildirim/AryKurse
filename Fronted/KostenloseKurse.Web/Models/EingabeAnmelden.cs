using System.ComponentModel.DataAnnotations;

namespace KostenloseKurse.Web.Models
{
    public class EingabeAnmelden
    {
        [Required]
        [Display(Name = "Ihre Emailadresse")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ihr Passwort")]
        public string Passwort{ get; set; }

        [Display(Name = "Erinnere dich an mich")]
        public bool ErinnereDich { get; set; }
    }
}
