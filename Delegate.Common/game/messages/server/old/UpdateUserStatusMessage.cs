using System;
using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class UpdateUserStatusMessage : ParsedMessage
    {
        internal UpdateUserStatusMessage(TeraMessageReader reader) : base(reader)
        {
            var message = reader.Message;
            Console.WriteLine(message);
            Console.WriteLine(reader.ReadEntityId());
            Console.WriteLine(reader.Read());
            Console.WriteLine("");
        }
    }
}
