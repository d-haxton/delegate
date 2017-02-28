namespace Delegate.Tera.Common.game.services
{
    public class Skill
    {
        internal Skill(int id, string name, bool? isChained = null, string detail = "")
        {
            Id = id;
            Name = name;
            IsChained = isChained;
            Detail = detail;
        }

        public int Id { get; }
        public string Name { get; }
        public bool? IsChained { get; }
        public string Detail { get; }

        public override string ToString()
        {
            return Detail;
        }
    }
}