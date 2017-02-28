using System.Collections.Generic;
using System.Linq;
using Delegate.Tera.Common.game;
using Delegate.Tera.Network.interfaces;

namespace Delegate.Tera.Network
{
    public class TeraServerList : ITeraServerList
    {
        public IEnumerable<Server> Servers { get; set; }

        public TeraServerList(string serverList)
        {
            Servers = GetServers(serverList);
        }

        private static IEnumerable<Server> GetServers(string serverList)
        {
            return serverList.Split(',')
                       .Where(s => !s.StartsWith("#") && !string.IsNullOrWhiteSpace(s))
                       .Select(s => s.Split(new[] { ' ' }, 3))
                       .Select(parts => new Server(parts[2], parts[1], parts[0]));
        }
    }
}
