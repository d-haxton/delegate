using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Delegate.Meter.interfaces;
using Delegate.Tera.Common.game;
using Delegate.Tera.Common.game.services;

namespace Delegate.Meter.meter.data
{
    public class TeraData : ITeraData
    {
        public TeraData(string version)
        {
            SkillDatabase = new SkillDatabase($"{Directory.GetCurrentDirectory()}\\Resources\\");
            NpcDatabase = new NpcDatabase(npcList.ToList());
            OpCodeNamer = new OpCodeNamer($"{Directory.GetCurrentDirectory()}\\Resources\\{version}.txt");
            SysMessageNamer = new OpCodeNamer($"{Directory.GetCurrentDirectory()}\\Resources\\smt_{version}.txt");
        }

        private IEnumerable<NpcInfo> npcList
        {
            get
            {
                // todo:: localization
                var xml = XDocument.Load(Path.Combine($"{Directory.GetCurrentDirectory()}\\Resources\\", "monsters\\monsters-NA.xml"));
                
                foreach (var zone in xml.Root.Elements("Zone"))
                {
                    var huntingZoneId = ushort.Parse(zone.Attribute("id").Value);
                    var area = zone.Attribute("name").Value;
                    foreach (var monsters in zone.Elements("Monster"))
                    {
                        var templateid = uint.Parse(monsters.Attribute("id").Value);
                        var boss = bool.Parse(monsters.Attribute("isBoss").Value);
                        var hp = long.Parse(monsters.Attribute("hp").Value);
                        var name = monsters.Attribute("name").Value;
                        yield return new NpcInfo(huntingZoneId, templateid, boss, hp, name, area);
                    }

                }
            }
        }

        public OpCodeNamer SysMessageNamer { get; }

        public OpCodeNamer OpCodeNamer { get; }
        public SkillDatabase SkillDatabase { get; }
        public NpcDatabase NpcDatabase { get; }
    }
}