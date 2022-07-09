using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Bestellung.Anwendung.Kartierung
{
    public static class ObjektMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var konfig = new MapperConfiguration(konfig =>
            {
                konfig.AddProfile<BrauchKartierung>();
            });

            return konfig.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
