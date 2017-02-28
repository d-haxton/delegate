using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SAbnormalityEnd : ParsedMessage
    {
        internal SAbnormalityEnd(TeraMessageReader reader) : base(reader)
        {
            TargetId = reader.ReadEntityId();
            AbnormalityId = reader.ReadInt32();
        }

        public int AbnormalityId { get; }

        public EntityId TargetId { get; }
    }
}