using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_LEAVE_PARTY : ParsedMessage
    {
        internal S_LEAVE_PARTY(TeraMessageReader reader) : base(reader)
        {
        }
    }
}