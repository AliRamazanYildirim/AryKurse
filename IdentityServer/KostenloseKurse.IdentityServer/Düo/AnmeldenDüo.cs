using System.ComponentModel.DataAnnotations;

namespace KostenloseKurse.IdentityServer.Düo
{
    public class AnmeldenDüo
    {
        [Required]
        public string BenutzerName { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Passwort { get; set; }
        [Required]
        public string City { get; set; }

    }
}
