using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.events;

namespace Delegate.Meter.interfaces
{
    public interface IDelegateCombatControl
    {
        event EventHandler<CombatUpdatedArgs> CombatUpdated;
        ObservableCollection<IDelegateBreakdown> CurrentCombat { get; }
        ObservableCollection<ObservableCollection<IDelegateBreakdown>> CombatHistory { get; }
        void CombatCheck();
        void UpdateCombat();
    }
}
