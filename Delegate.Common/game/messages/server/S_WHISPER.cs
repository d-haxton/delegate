﻿using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_WHISPER : ParsedMessage
    {
        internal S_WHISPER(TeraMessageReader reader) : base(reader)
        {
            reader.Skip(17);
            Sender = reader.ReadTeraString();
            Receiver = reader.ReadTeraString();
            Text = reader.ReadTeraString();
        }

        public string Sender { get; set; }
        public string Receiver { get; set; }

        public string Text { get; set; }

        public byte[] Canal { get; set; }
    }
}