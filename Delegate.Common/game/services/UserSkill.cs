namespace Delegate.Tera.Common.game.services
{
    public class UserSkill : Skill
    {
        public UserSkill(int id, RaceGenderClass raceGenderClass, string name, bool? isChained = null,
            string detail = "") : base(id, name, isChained, detail)
        {
            RaceGenderClass = raceGenderClass;
        }

        public RaceGenderClass RaceGenderClass { get; }

        public override bool Equals(object obj)
        {
            var other = obj as UserSkill;
            if (other == null)
            {
                return false;
            }
            return Id == other.Id && RaceGenderClass.Equals(other.RaceGenderClass);
        }

        public override int GetHashCode()
        {
            return Id + RaceGenderClass.GetHashCode();
        }
    }
}