using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.client
{
    public class C_CHECK_VERSION : ParsedMessage
    {
        internal C_CHECK_VERSION(TeraMessageReader reader) : base(reader)
        {
            var count = reader.ReadUInt16();
            var offset = reader.ReadUInt16();
            for (var i = 1; i <= count; i++)
            {
                reader.BaseStream.Position = offset - 4;
                var pointer = reader.ReadUInt16();
                Debug.Assert(pointer == offset);//should be the same
                var nextOffset = reader.ReadUInt16();
                var VersionKey = reader.ReadUInt32();
                var VersionValue = reader.ReadUInt32();
                Versions.Add(VersionKey, VersionValue);
                offset = nextOffset;
            }
        }

        public Dictionary<uint, uint> Versions { get; } = new Dictionary<uint, uint>();
    }
}
