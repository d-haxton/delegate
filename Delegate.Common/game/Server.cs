namespace Delegate.Tera.Common.game
{
    public class Server
    {
        public string Ip { get; private set; }
        public string Name { get; private set; }
        public string Region { get; private set; }

        public Server(string name, string region, string ip)
        {
            Ip = ip;
            Name = name;
            Region = region;
        }
    }
}
