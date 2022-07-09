using KostenloseKurse.Dienste.Bestellung.Anwendung.Anfragen;
using KostenloseKurse.Dienste.Bestellung.Anwendung.Düo;
using KostenloseKurse.Dienste.Bestellung.Anwendung.Kartierung;
using KostenloseKurse.Dienste.Bestellung.Infrastruktur;
using KostenloseKurse.Shared.Düo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Handler
{
    public class AbrufenVonBestellungenNachBenutzerIDAbfrageHandler : IRequestHandler<AbrufenVonBestellungenNachBenutzerIDAbfrage, Antwort<List<BestellungsDüo>>>
    {
        private readonly BestellungDbKontext _kontext;

        public AbrufenVonBestellungenNachBenutzerIDAbfrageHandler(BestellungDbKontext kontext)
        {
            _kontext = kontext;
        }

        public async Task<Antwort<List<BestellungsDüo>>> Handle(AbrufenVonBestellungenNachBenutzerIDAbfrage request, CancellationToken cancellationToken)
        {
            var bestellungen = await _kontext.Bestellungen.Include(x => x.BestellungsArtikel).Where(x => x.AbnehmerID == request.BenutzerID).ToListAsync();

            if (!bestellungen.Any())
            {
                return Antwort<List<BestellungsDüo>>.Erfolg(new List<BestellungsDüo>(), 200);
            }

            var bestellungsDüo = ObjektMapper.Mapper.Map<List<BestellungsDüo>>(bestellungen);

            return Antwort<List<BestellungsDüo>>.Erfolg(bestellungsDüo, 200);
        }
    }
}
