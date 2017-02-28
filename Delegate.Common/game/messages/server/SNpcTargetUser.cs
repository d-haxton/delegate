using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SNpcTargetUser : ParsedMessage

    {
        internal SNpcTargetUser(TeraMessageReader reader) : base(reader)
        {
            NPC = reader.ReadEntityId();
        }

        public EntityId NPC { get; private set; }
    }
}