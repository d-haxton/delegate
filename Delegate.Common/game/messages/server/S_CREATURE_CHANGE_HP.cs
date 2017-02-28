﻿using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SCreatureChangeHp : ParsedMessage
    {
        internal SCreatureChangeHp(TeraMessageReader reader) : base(reader)
        {
            HpRemaining = reader.ReadInt32();
            TotalHp = reader.ReadInt32();
            HpChange = reader.ReadInt16();
            Type = reader.ReadInt32();
            Unknow3 = reader.ReadInt16();
            TargetId = reader.ReadEntityId();
            SourceId = reader.ReadEntityId();
            Critical = reader.ReadInt16();


            //           Debug.WriteLine("target = " + TargetId + ";Source:" + SourceId + ";Critical:" + Critical + ";Hp left:" + HpRemaining + ";Max HP:" + TotalHp+";HpLost/Gain:"+ HpChange + ";Type:"+ Type + ";Unknow3:"+Unknow3);
        }

        public int Unknow3 { get; }
        public int HpChange { get; }

        public int Type { get; }


        public int HpRemaining { get; }

        public int TotalHp { get; }

        public int Critical { get; }


        public EntityId TargetId { get; }
        public EntityId SourceId { get; }
        public bool Slaying => TotalHp > HpRemaining*2 && HpRemaining > 0;
    }
}