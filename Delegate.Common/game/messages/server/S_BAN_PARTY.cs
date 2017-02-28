using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_BAN_PARTY : ParsedMessage
    {
        internal S_BAN_PARTY(TeraMessageReader reader) : base(reader)
        {
        }
    }
}