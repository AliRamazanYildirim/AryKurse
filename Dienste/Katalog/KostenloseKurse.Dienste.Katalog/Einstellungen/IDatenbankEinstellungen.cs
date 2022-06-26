namespace KostenloseKurse.Dienste.Katalog.Einstellungen
{
    public interface IDatenbankEinstellungen
    {
        public string KursKollektionName { get; set; }
        public string KategorieKollektionName { get; set; }
        public string AnschlussString { get; set; }
        public string DatenbankName { get; set; }
    }
}
