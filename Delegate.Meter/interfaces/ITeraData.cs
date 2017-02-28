using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Tera.Common.game;
using Delegate.Tera.Common.game.services;

namespace Delegate.Meter.interfaces
{
    public interface ITeraData
    {
        OpCodeNamer OpCodeNamer { get; }
        SkillDatabase SkillDatabase { get; }
        NpcDatabase NpcDatabase { get; }
    }
}
