﻿using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SNpcOccupierInfo : ParsedMessage

    {
        internal SNpcOccupierInfo(TeraMessageReader reader) : base(reader)
        {
            //  PrintRaw();
            NPC = reader.ReadEntityId();
            Engager = reader.ReadEntityId();
            Target = reader.ReadEntityId();

            //  Debug.WriteLine("NPC:" + NPC + ";Target:" + Target);
        }

        public EntityId NPC { get; }
        public EntityId Engager { get; }
        public EntityId Target { get; }
    }
}