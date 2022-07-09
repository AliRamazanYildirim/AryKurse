using KostenloseKurse.Dienste.Bestellung.Anwendung.Düo;
using KostenloseKurse.Shared.Düo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Befehle
{
    public class BestellungsBefehlErstellen: IRequest<Antwort<BestellungErstellenDüo>>
    {
        public string AbnehmerID { get; set; }

        public List<BestellungsArtikelDüo> BestellungsArtikel { get; set; }

        public AdresseDüo Addresse { get; set; }
    }
}
