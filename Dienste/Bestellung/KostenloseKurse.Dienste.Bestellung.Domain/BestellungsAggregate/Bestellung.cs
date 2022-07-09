using KostenloseKurse.Dienste.Bestellung.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate
{
    public class Bestellung:Einheit,IAggregateRoot
    {
        //EF Core features
        // -- Owned Types
        // -- Shadow Property
        // -- Backing Field
        public DateTime ErstellungsDatum { get; private set; }

        public Adresse Adresse { get; private set; }

        public string AbnehmerID { get; private set; }

        private readonly List<BestellungsArtikel> _bestellungen;

        public IReadOnlyCollection<BestellungsArtikel> BestellungsArtikel => _bestellungen;

        public Bestellung()
        {
        }

        public Bestellung(string abnehmerID, Adresse adresse)
        {
            _bestellungen = new List<BestellungsArtikel>();
            ErstellungsDatum = DateTime.Now;
            AbnehmerID = abnehmerID;
            Adresse = adresse;
        }

        public void BestellungsArtikelHinzufügen(string produktID, string produktName, decimal preis, string bildUrl)
        {
            var existiertesProdukt = _bestellungen.Any(x => x.ProduktID == produktID);

            if (!existiertesProdukt)
            {
                var neuesBestellungsArtikel = new BestellungsArtikel(produktID, produktName, bildUrl, preis);

                _bestellungen.Add(neuesBestellungsArtikel);
            }
        }

        public decimal GesamtpreisAufrufen => _bestellungen.Sum(x => x.Preis);
    }
}
