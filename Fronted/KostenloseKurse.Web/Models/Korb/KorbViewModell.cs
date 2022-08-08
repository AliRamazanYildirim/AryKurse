using System;
using System.Collections.Generic;
using System.Linq;

namespace KostenloseKurse.Web.Models.Korb
{
    public class KorbViewModell
    {
        public KorbViewModell()
        {
            _korbGegenstande = new List<KorbGegenstandViewModell>();
        }
        public string BenuzterID { get; set; }
        public string RabattCode { get; set; }
        public int? RabattRate { get; set; }

        private List<KorbGegenstandViewModell> _korbGegenstande;

        public List<KorbGegenstandViewModell> KorbGegenstande
        {
            get
            {
                if (GabRabatt)
                {
                    //Beispielkurspreis 50 € Rabatt 10%
                    _korbGegenstande.ForEach(x =>
                    {
                        var rabattPreis = x.Preis * ((decimal)RabattRate.Value / 100);
                        x.AngewandterRabatt(Math.Round(x.Preis - rabattPreis, 2));//45.00 €
                    });
                }
                return _korbGegenstande;
            }
            set
            {
                _korbGegenstande = value;
            }
        }
        public decimal Gesamtpreis 
        { 
            get => _korbGegenstande.Sum(x => x.RufAktuellenPreisAuf);

        }
        
        public bool GabRabatt
        {
            get => !string.IsNullOrEmpty(RabattCode) && RabattRate.HasValue;
        }

        public void RabattStornieren()
        {
            RabattCode = null;
            RabattRate = null;
        }

        public void AngewandterRabatt(string Code, int Rate)
        {
            RabattCode = Code;
            RabattRate = Rate;
        }
    }
}
