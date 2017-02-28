using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Tera.Common.game;
using Delegate.Tera.Common.game.services;

namespace Delegate.Meter.interfaces
{
    public interface ISkillResult
    {
        DateTime Time { get; }
        int Amount { get; }
        Entity Source { get; }
        Entity Target { get; }
        bool IsCritical { get; }
        bool IsHeal { get; }
        int SkillId { get; }
        Skill Skill { get; }
        string SkillName { get; }
        string SkillNameDetailed { get; }
        bool? IsChained { get; }
        int Damage { get; }
        int Heal { get; }
        Player SourcePlayer { get; }
        Player TargetPlayer { get; }
        string ToString();
    }
}
