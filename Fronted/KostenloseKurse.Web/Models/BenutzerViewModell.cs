using System.Collections.Generic;

namespace KostenloseKurse.Web.Models
{
    public class BenutzerViewModell
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public IEnumerable<string> RufBenutzerNachEigenschaftenAuf()
        {
            yield return UserName;
            yield return Email;
            yield return City;
        }
    }
}
