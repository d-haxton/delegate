using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.events;
using ReactiveUI;

namespace Delegate.Meter.interfaces
{
    public interface IDelegateCombatControl
    {
        event EventHandler<CombatUpdatedArgs> CombatUpdated;
        ReactiveList<IDelegateBreakdown> CurrentCombat { get; }
        ReactiveList<ReactiveList<IDelegateBreakdown>> CombatHistory { get; }
        void CombatCheck();
        void UpdateCombat();
    }
}
