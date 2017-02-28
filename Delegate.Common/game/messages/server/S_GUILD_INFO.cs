﻿using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_GUILD_INFO : ParsedMessage
    {
        internal S_GUILD_INFO(TeraMessageReader reader) : base(reader)
        {
            reader.Skip(4);
            var offset = reader.ReadUInt16();
            reader.BaseStream.Position = offset - 4;
            GuildName = reader.ReadTeraString();
        }

        public string GuildName { get; }
    }
}
