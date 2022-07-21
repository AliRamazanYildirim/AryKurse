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
        public int? DiskontSatz { get; set; }
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
                        var rabattPreis = x.Preis * ((decimal)DiskontSatz.Value / 100);
                        x.AngewandterRabatt(Math.Round(x.Preis - rabattPreis, 2));//90.00 €
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
            get => !string.IsNullOrEmpty(RabattCode) && DiskontSatz.HasValue;
        }

        public void RabattStornieren()
        {
            RabattCode = null;
            DiskontSatz = null;
        }

        public void AngewandterRabatt(string code, int satz)
        {
            RabattCode = code;
            DiskontSatz = satz;
        }
    }
}
