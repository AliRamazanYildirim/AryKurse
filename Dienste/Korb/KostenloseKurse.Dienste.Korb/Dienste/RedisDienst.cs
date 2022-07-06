using StackExchange.Redis;

namespace KostenloseKurse.Dienste.Korb.Dienste
{
    public class RedisDienst
    {
        private readonly string _host;
        private readonly int _port;

        private ConnectionMultiplexer _verbindungsmultiplexer;

        public RedisDienst(string host,int port)
        {
            _host=host;
            _port = port;
            
        }
        public void Verbinden() => _verbindungsmultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase RufDB(int db = 1) => _verbindungsmultiplexer.GetDatabase(db);
    }
}
