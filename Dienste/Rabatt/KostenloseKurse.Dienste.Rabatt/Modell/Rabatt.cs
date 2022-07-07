using System;

namespace KostenloseKurse.Dienste.Rabatt.Modell
{
    [Dapper.Contrib.Extensions.Table("rabatt")]
    public class Rabatt
    {
        public int ID { get; set; }
        public string BenutzerID { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime ErstellungsDatum  { get; set; }
    }
}
