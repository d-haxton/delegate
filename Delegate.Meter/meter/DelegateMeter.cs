using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.events;
using Delegate.Meter.interfaces;

namespace Delegate.Meter.meter
{
    public class DelegateMeter : IDelegateMeter
    {
        private readonly IDelegateCombatControl _combatControl;
        public event EventHandler<CombatUpdatedArgs> CombatUpdatedEvent = delegate { }; 
        public DelegateMeter(IDelegateNetworkWrapper networkWrapper, IDelegateCombatControl combatControl)
        {
            _combatControl = combatControl;

            networkWrapper.SkillEvent += OnSkillEvent;
            combatControl.CombatUpdated += CombatEventHandler;
        }

        private void CombatEventHandler(object sender, CombatUpdatedArgs e)
        {
            CombatUpdatedEvent?.Invoke(sender, e);
        }

        private void OnSkillEvent(object sender, SkillEventArgs e)
        {
            // return if did 0 damage
            if (e.SkillResult.IsHeal == false && e.SkillResult.Damage <= 0)
            {
                return;
            }
            _combatControl.CombatCheck();

            var doesPlayerExistInCurrentCombat = false;

            foreach (var combatBreakdown  in _combatControl.CurrentCombat)
            {
                if (combatBreakdown.Source == e.SkillResult.SourcePlayer.Name)
                {
                    combatBreakdown.ActUponSkillEvent(e.SkillResult, true);
                    doesPlayerExistInCurrentCombat = true;
                }
                else if (combatBreakdown.Source == e.SkillResult?.TargetPlayer?.Name)
                {
                    combatBreakdown.ActUponSkillEvent(e.SkillResult, false);
                }
            }
            if(doesPlayerExistInCurrentCombat == false)
            {
                var breakdown = new DelegateBreakdown();
                breakdown.ActUponSkillEvent(e.SkillResult, true);
                _combatControl.CurrentCombat.Add(breakdown);
            }
            var damage = _combatControl.CurrentCombat.Sum(breakdown => breakdown.Damage);
            foreach (var breakdown in _combatControl.CurrentCombat)
            {
                breakdown.DamagePercent = Math.Truncate(10 *(double)damage/breakdown.Damage) / 10;
            }
            _combatControl.UpdateCombat();
        }
    }
}
