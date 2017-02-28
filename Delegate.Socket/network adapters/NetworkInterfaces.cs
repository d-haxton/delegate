using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Delegate.Socket.interfaces;

namespace Delegate.Socket.network_adapters
{
    public class NetworkInterfaces : INetworkInterfaces
    {
        public List<string> Networks => GetNetworkInterfaces();

        private List<string> GetNetworkInterfaces()
        {
            var listOfIp = new List<string>();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            var ipOnly = ip.Address.ToString().Split(':');
                            listOfIp.Add(ipOnly[0]);
                        }
                    }
                }
            }
            return listOfIp;
        } 
    }
}
