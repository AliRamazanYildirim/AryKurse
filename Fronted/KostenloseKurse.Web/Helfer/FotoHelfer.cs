using KostenloseKurse.Web.Models;
using Microsoft.Extensions.Options;

namespace KostenloseKurse.Web.Helfer
{
    public class FotoHelfer
    {
        private readonly DienstApiEinstellungen _dienstApiEinstellungen;

        public FotoHelfer(IOptions<DienstApiEinstellungen> dienstApiEinstellungen)
        {
            _dienstApiEinstellungen = dienstApiEinstellungen.Value;
        }

        public string RufFotoBestandUrlAuf(string bildUrl)
        {
            return $"{_dienstApiEinstellungen.FotoBestandUri}/bilder/{bildUrl}";
        }
    }
}
