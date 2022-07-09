using AutoMapper;
using KostenloseKurse.Dienste.Bestellung.Anwendung.Düo;
using KostenloseKurse.Dienste.Bestellung.Domain.BestellungsAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Kartierung
{
    public class BrauchKartierung:Profile
    {
        public BrauchKartierung()
        {
            CreateMap<Domain.BestellungsAggregate.Bestellung, BestellungsDüo>().ReverseMap();
            CreateMap<BestellungsArtikel, BestellungsArtikelDüo>().ReverseMap();
            CreateMap<Adresse, AdresseDüo>().ReverseMap();
        }
    }
}
