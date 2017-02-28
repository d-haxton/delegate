using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Delegate.Meter.events;

namespace Delegate.Meter.interfaces
{
    public interface IDelegateBreakdown
    {
        string Source { get; }
        string Target { get; }
        int Damage { get; }
        int DamagePerSecond { get; }
        double DamagePercent { get; set; }
        double CriticalPercent { get; }
        int Hits { get; }
        int HitsCritical { get; }
        int Healing { get; }
        int HealingPerSecond { get; }
        ConcurrentBag<ISkillResult> History { get; }
        void ActUponSkillEvent(ISkillResult skillResult, bool isSource);
        DateTime Time { get; }
        bool IsFinished { get; set; }
    }
}
