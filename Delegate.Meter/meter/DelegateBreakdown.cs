using System;
using System.Collections.Concurrent;
using System.Linq;
using Delegate.Meter.interfaces;
using Delegate.Tera.Common.game;

namespace Delegate.Meter.meter
{
    public class DelegateBreakdown : IDelegateBreakdown
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public int Damage => Convert.ToInt32(DamageFullValue);
        private double DamageFullValue => History.Sum(breakdown => breakdown.Damage);
        private double DamageTaken { get; set; }
        public int DamagePerSecond => Convert.ToInt32(DamageFullValue/SecondsSinceStart);
        public double DamagePercent { get; set; }
        public double CriticalPercent => Math.Truncate((double) HitsCritical/HitsTotal * 1000)/10;
        public int Hits { get; set; }
        public int HitsCritical { get; set; }
        private int HitsTotal => Hits + HitsCritical;
        public int Healing => Convert.ToInt32(HealingFullValue);
        public double HealingFullValue => History.Sum(breakdown => breakdown.Heal);
        public int HealingPerSecond => Convert.ToInt32(HealingFullValue / SecondsSinceStart);
        public ConcurrentBag<ISkillResult> History { get; }
        private DateTime StartFight { get; set; }
        private DateTime _time;
        public DateTime Time => IsFinished ? _time : (_time = DateTime.Now);
        public bool IsFinished { get; set; }
        private int SecondsSinceStart => Time.Subtract(StartFight).Seconds + 1;

        public DelegateBreakdown()
        {
            // initialize to 0s
            Hits = 0;
            HitsCritical = 0;
            IsFinished = false;
            StartFight = DateTime.MinValue;
            History = new ConcurrentBag<ISkillResult>();
        }

        public void ActUponSkillEvent(ISkillResult skillResult, bool isSource)
        {
            CheckFightTime();

            if (isSource)
                ActUponSourceSkill(skillResult);
            else
                ActUponTargetSkill(skillResult);

            History.Add(skillResult);
        }

        private void CheckFightTime()
        {
            if (StartFight == DateTime.MinValue)
                StartFight = DateTime.Now;
        }
        private void ActUponTargetSkill(ISkillResult skillResult)
        {
            ParseDamageTaken(skillResult);

            Source = FindTarget(skillResult);
            Target = skillResult.SourcePlayer.Name;
        }

        private void ParseDamageTaken(ISkillResult skillResult)
        {
            if(skillResult.Damage > 0)
                DamageTaken += skillResult.Damage;
        }

        private void ActUponSourceSkill(ISkillResult skillResult)
        {
            ParseDamage(skillResult);

            Target = FindTarget(skillResult);
            Source = skillResult.SourcePlayer.Name;
        }
        private string FindTarget(ISkillResult skillResult)
        {
            if (skillResult.TargetPlayer?.Name.Length > 3)
                return skillResult.TargetPlayer.Name;
            var targetNpc = skillResult.Target as NpcEntity;
            if (targetNpc?.Info.Name.Length > 3)
            {
                return targetNpc.Info.Name;
            }
            return skillResult.Target.ToString();
        }

        private void ParseDamage(ISkillResult skillResult)
        {
            if (skillResult.IsCritical)
            {
                HitsCritical++;
            }
            else
            {
                Hits++;
            }
        }

    }
}
