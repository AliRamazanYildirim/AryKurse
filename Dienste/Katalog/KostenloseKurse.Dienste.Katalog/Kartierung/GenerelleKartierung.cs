using AutoMapper;
using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Modelle;

namespace KostenloseKurse.Dienste.Katalog.Kartierung
{
    public class GenerelleKartierung:Profile
    {
        public GenerelleKartierung()
        {
            CreateMap<Kurs, KursDüo>().ReverseMap();
            CreateMap<Kategorie, KategorieDüo>().ReverseMap();
            CreateMap<Eigenschaft, EigenschaftDüo>().ReverseMap();

            CreateMap<Kurs, KursErstellenDüo>().ReverseMap();
            CreateMap<Kurs, KursAktualisierenDüo>().ReverseMap();
        }
    }
}
