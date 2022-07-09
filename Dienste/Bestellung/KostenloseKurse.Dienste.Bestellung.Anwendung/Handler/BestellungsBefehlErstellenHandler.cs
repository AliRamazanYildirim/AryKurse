using KostenloseKurse.Dienste.Bestellung.Anwendung.Befehle;
using KostenloseKurse.Dienste.Bestellung.Anwendung.Düo;
using KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate;
using KostenloseKurse.Dienste.Bestellung.Infrastruktur;
using KostenloseKurse.Shared.Düo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Handler
{
    public class BestellungsBefehlErstellenHandler : IRequestHandler<BestellungsBefehlErstellen, Antwort<BestellungErstellenDüo>>
    {
        private readonly BestellungDbKontext _kontext;

        public BestellungsBefehlErstellenHandler(BestellungDbKontext kontext)
        {
            _kontext = kontext;
        }

        public async Task<Antwort<BestellungErstellenDüo>> Handle(BestellungsBefehlErstellen request, CancellationToken cancellationToken)
        {
            var neueAdresse = new Adresse(request.Addresse.Provinz, request.Addresse.Gebiet, request.Addresse.Strasse,
                request.Addresse.PostleitZahl, request.Addresse.Linie);

            Domain.BestellungsAggregate.Bestellung neueBestellung = new Domain.BestellungsAggregate.Bestellung(request.AbnehmerID, neueAdresse);

            request.BestellungsArtikel.ForEach(x =>
            {
                neueBestellung.BestellungsArtikelHinzufügen(x.ProduktID, x.ProduktName, x.Preis, x.BildUrl);
            });

            await _kontext.Bestellungen.AddAsync(neueBestellung);

            await _kontext.SaveChangesAsync();

            return Antwort<BestellungErstellenDüo>.Erfolg(new BestellungErstellenDüo { BestellungID = neueBestellung.Id }, 200);
        }
    }
}
