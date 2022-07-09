using KostenloseKurse.Dienste.Bestellung.Anwendung.Düo;
using KostenloseKurse.Shared.Düo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Anfragen
{
    public class AbrufenVonBestellungenNachBenutzerIDAbfrage:IRequest<Antwort<List<BestellungsDüo>>>
    {
        public string BenutzerID { get; set; }
    }
}
