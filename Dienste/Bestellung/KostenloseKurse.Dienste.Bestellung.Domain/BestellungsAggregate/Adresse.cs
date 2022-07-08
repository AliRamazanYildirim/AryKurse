using KostenloseKurse.Dienste.Bestellung.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate
{
    public class Adresse:WertObjekt
    {
        public string Provinz { get; private set; }

        public string Gebiet { get; private set; }

        public string Strasse { get; private set; }

        public string PostleitZahl { get; private set; }

        public string Linie { get; private set; }

        public Adresse(string provinz, string gebiet, string strasse, string postleitZahl, string linie)
        {
            Provinz = provinz;
            Gebiet = gebiet;
            Strasse = strasse;
            PostleitZahl = postleitZahl;
            Linie = linie;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Provinz;
            yield return Gebiet;
            yield return Strasse;
            yield return PostleitZahl;
            yield return Linie;
        }

    }
}
