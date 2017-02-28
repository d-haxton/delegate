using System;
using System.Collections.ObjectModel;
using Delegate.Meter.events;
using Delegate.Meter.interfaces;
using ReactiveUI;

namespace Delegate.Meter.meter
{
    public class DelegateCombatControl: IDelegateCombatControl
    {
        public event EventHandler<CombatUpdatedArgs> CombatUpdated = delegate { }; 
        private const int CombatTimer = 10;
        private DateTime LastUpdated { get; set; }
        public ReactiveList<IDelegateBreakdown> CurrentCombat { get; set; }
        public ReactiveList<ReactiveList<IDelegateBreakdown>> CombatHistory { get; }

        public void CombatCheck()
        {
            var seconds = DateTime.Now.Subtract(LastUpdated).Seconds;
            if (seconds > CombatTimer && LastUpdated != DateTime.MinValue)
            {
                foreach (var breakdown in CurrentCombat)
                {
                    breakdown.IsFinished = true;
                }

                var newDict = new ReactiveList<IDelegateBreakdown>();
                CombatHistory.Add(newDict);
                CurrentCombat = newDict;
            }
            LastUpdated = DateTime.Now;
        }

        public void UpdateCombat()
        {
            CombatUpdated?.Invoke(this, null);
        }

        public DelegateCombatControl()
        {
            LastUpdated = DateTime.MinValue;
            CurrentCombat = new ReactiveList<IDelegateBreakdown>()
            {
                ChangeTrackingEnabled = true
            };
            CombatHistory = new ReactiveList<ReactiveList<IDelegateBreakdown>> {CurrentCombat};
        }
    }
}
