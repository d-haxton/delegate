using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SPartyMemberAbnormalClear : ParsedMessage
    {
        internal SPartyMemberAbnormalClear(TeraMessageReader reader) : base(reader)
        {
            //  PrintRaw();
        }
    }
}