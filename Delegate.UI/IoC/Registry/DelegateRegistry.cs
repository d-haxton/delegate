using Delegate.Meter.interfaces;
using Delegate.Meter.meter;
using Delegate.Socket.interfaces;
using Delegate.Socket.network_adapters;
using Delegate.Socket.sockets;
using Delegate.Tera.Network;
using Delegate.Tera.Network.interfaces;
using Delegate.UI.dependencies;
using Delegate.UI.interfaces;
using Delegate.UI.Model;
using Delegate.UI.Properties;

namespace Delegate.UI.IoC.Registry
{
    public class DelegateRegistry : StructureMap.Registry
    {
        public DelegateRegistry()
        {
            For<IMeterUiModel>().Use<MeterUiModel>();
            For<IDelegateMeter>().Use<DelegateMeter>();
            For<IDelegateBreakdown>().Use<DelegateBreakdown>();
            For<IDelegateCombatControl>().Use<DelegateCombatControl>().Singleton();
            For<IDelegateNetworkWrapper>().Use<DelegateNetworkWrapper>().Singleton();
            For<IDelegateSniffer>().Use<DelegateSniffer>();
            For<ISniffer>().Use<SocketSniffer>();
            For<ISocketSettings>().Use<SocketSettings>().Singleton();
            For<ISocketWrapper>().Use<SocketWrapper>();
            For<INetworkInterfaces>().Use<NetworkInterfaces>();
            For<ITeraServerList>().Use<TeraServerList>().Ctor<string>().Is(Resources.Servers);
            For<ISettingsWrapper>().Use<SettingsWrapper>().Singleton();
        }
    }
}