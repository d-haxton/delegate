using System.Collections.Generic;
using Delegate.Tera.Common.game;

namespace Delegate.Tera.Network.interfaces
{
    public interface ITeraServerList
    {
        IEnumerable<Server> Servers { get; }
    }
}
