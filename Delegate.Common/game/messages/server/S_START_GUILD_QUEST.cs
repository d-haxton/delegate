using System;
using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_START_GUILD_QUEST : ParsedMessage
    {
        public uint QuestId { get; private set; }
        public string Guildname { get; private set; }
        internal S_START_GUILD_QUEST(TeraMessageReader reader) : base(reader)
        {
            var unkown = reader.ReadUInt16();
            var unknowByte = reader.ReadByte();
            QuestId = reader.ReadUInt32();
            Guildname = reader.ReadTeraString();
            Console.WriteLine("id:"+ QuestId + ";guildname:"+ Guildname + ";unkown:" + unkown + ";unknown:"+ unknowByte);
        }
    }
}
