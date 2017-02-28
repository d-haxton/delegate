using System;
using System.Collections.Generic;
using System.Linq;
using Delegate.GhettoTest.Properties;
using Delegate.Meter.events;
using Delegate.Meter.meter;
using Delegate.Socket.network_adapters;
using Delegate.Socket.sockets;
using Delegate.Tera.Common.game;
using Delegate.Tera.Network;

namespace Delegate.GhettoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var ni = new NetworkInterfaces();
            var settings = new SocketSettings(ni);
            var wrapper = new SocketWrapper(settings);
            var sniffer = new SocketSniffer(wrapper);
            var serverList = GetServers(Resources.Servers);
            var teraSniffer = new DelegateSniffer(sniffer, new TeraServerList(Resources.Servers));

            var meter = new DelegateNetworkWrapper(teraSniffer);
            var cc = new DelegateCombatControl();
            var parse = new DelegateMeter(meter, cc);
            parse.CombatUpdatedEvent += OnUpdated;
            teraSniffer.MessageReceived += HandleMessageReceived;
            teraSniffer.NewConnection += HandleNewConnection;
            Console.ReadLine();
        }

        private static void HandleNewConnection(Server obj)
        {
            Console.WriteLine(obj.Ip);
        }

        private static void HandleMessageReceived(Message obj)
        {
            Console.WriteLine(obj.Time);
        }

        private static void OnUpdated(object sender, CombatUpdatedArgs e)
        {
            Console.WriteLine("updated combat event");
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
