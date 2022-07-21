namespace KostenloseKurse.Web.Models.Korb
{
    public class KorbGegenstandViewModell
    {
        public int Menge { get; set; } = 1;
        public string KursID { get; set; }
        public string KursName { get; set; }
        public decimal Preis { get; set; }
        private decimal? RabattAngewendeterPreis { get; set; }
        public decimal RufAktuellenPreisAuf
        {
            get => RabattAngewendeterPreis != null ? RabattAngewendeterPreis.Value : Preis;
        }

        public void AngewandterRabatt(decimal rabattPreis)
        {
            RabattAngewendeterPreis = rabattPreis;
        }
    }
}
