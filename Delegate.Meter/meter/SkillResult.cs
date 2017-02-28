using System;
using Delegate.Meter.interfaces;
using Delegate.Tera.Common.game;
using Delegate.Tera.Common.game.messages.server;
using Delegate.Tera.Common.game.services;
using UserSkill = Delegate.Tera.Common.game.UserSkill;

namespace Delegate.Meter.meter
{
    public class SkillResult : ISkillResult
    {
        public SkillResult(EachSkillResultServerMessage message, EntityTracker entityRegistry,
            PlayerTracker playerTracker, SkillDatabase skillDatabase)
        {
            Time = message.Time;
            Amount = message.Amount;
            IsCritical = message.IsCritical;
            IsHeal = message.IsHeal;
            SkillId = message.SkillId;

            Source = entityRegistry.GetOrPlaceholder(message.Source);
            Target = entityRegistry.GetOrPlaceholder(message.Target);
            var userNpc = UserEntity.ForEntity(Source);
            var npc = (NpcEntity) userNpc["source"];
            var sourceUser = userNpc["root_source"] as UserEntity; // Attribute damage dealt by owned entities to the owner
            var targetUser = Target as UserEntity; // But don't attribute damage received by owned entities to the owner

            if (sourceUser != null)
            {
                Skill = skillDatabase.Get(sourceUser, message);
                if (Skill == null && npc != null)
                {
                    Skill = new UserSkill(message.SkillId, sourceUser.RaceGenderClass, npc.Info.Name);
                }
                SourcePlayer = playerTracker.Get(sourceUser.PlayerId);
                if (Skill == null)
                {
                    Skill = new UserSkill(message.SkillId, sourceUser.RaceGenderClass, "Unknown");
                }
            }
            if (targetUser != null)
            {
                TargetPlayer = playerTracker.Get(targetUser.PlayerId);
            }
        }

        public DateTime Time { get; }

        public int Amount { get; }
        public Entity Source { get; }
        public Entity Target { get; }
        public bool IsCritical { get; }
        public bool IsHeal { get; }
        public int SkillId { get; }
        public Skill Skill { get; }
        public string SkillName => Skill?.Name ?? SkillId.ToString();

        public string SkillNameDetailed => $"{Skill?.Name ?? SkillId.ToString()} {(IsChained != null ? (bool) IsChained ? "[C]" : null : null)} {(Skill?.Detail == "" ? null : "(" + Skill?.Detail + ")")}";

        public bool? IsChained => Skill.IsChained;
        public int Damage => IsHeal ? 0 : Amount;
        public int Heal => IsHeal ? Amount : 0;


        public Player SourcePlayer { get; }
        public Player TargetPlayer { get; }

        public override string ToString()
        {
            return $"{SkillName}({SkillId}) [{Amount}]";
        }
    }
}