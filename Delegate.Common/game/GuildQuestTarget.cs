using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.Tera.Common.game
{
    public class GuildQuestTarget
    {

        public uint ZoneId { get; private set; }
        public uint TargetId { get; private set; }
        public uint CountQuest { get; private set; }
        public uint TotalQuest { get; private set; }

        public GuildQuestTarget(uint zoneId, uint targetId, uint countQuest, uint totalQuests)
        {

            ZoneId = zoneId;
            TargetId = targetId;
            CountQuest = countQuest;
            TotalQuest = totalQuests;

        }

        public override string ToString()
        {
            return "ZoneId:" + ZoneId + ";TargetId:" + TargetId + ";countQuest:" + CountQuest + ";totalQuest:" + TotalQuest;
        }
    }
}
