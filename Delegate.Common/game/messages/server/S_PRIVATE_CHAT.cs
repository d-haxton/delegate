using System;
using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_PRIVATE_CHAT : ParsedMessage
    {
        internal S_PRIVATE_CHAT(TeraMessageReader reader) : base(reader)
        {
            reader.Skip(4);//offsets
            Channel = reader.ReadUInt32();
            AuthorId = reader.ReadUInt64();
            AuthorName = reader.ReadTeraString();
            Text = reader.ReadTeraString();
            Console.WriteLine("Channel:"+Channel+";Username:"+AuthorName+";Text:"+Text+";AuthorId:"+AuthorId);
        }

        public string AuthorName { get; set; }

        public ulong AuthorId { get; set; }

        public string Text { get; set; }

        public uint Channel { get; set; }
    }
}