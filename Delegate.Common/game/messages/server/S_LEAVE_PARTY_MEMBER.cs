using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class S_LEAVE_PARTY_MEMBER : ParsedMessage
    {
        internal S_LEAVE_PARTY_MEMBER(TeraMessageReader reader) : base(reader)
        {
            var nameoffset = reader.ReadUInt16();
            ServerId = reader.ReadUInt32();
            PlayerId = reader.ReadUInt32();
            Name = reader.ReadTeraString();
        }

        public uint ServerId { get; }
        public uint PlayerId { get; }
        public string Name { get; }
    }
}