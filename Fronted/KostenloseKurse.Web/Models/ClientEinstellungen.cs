namespace KostenloseKurse.Web.Models
{
    public class ClientEinstellungen
    {
        public Client WebClient { get; set; }
        public Client WebClientFürBenutzer { get; set; }
    }
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
