using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SDespawnUser : ParsedMessage
    {
        internal SDespawnUser(TeraMessageReader reader) : base(reader)
        {
            User = reader.ReadEntityId();
        }

        public EntityId User { get; }
    }
}