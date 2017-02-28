using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.interfaces;
using Delegate.Meter.meter;

namespace Delegate.Meter.events
{
    public class SkillEventArgs : EventArgs
    {
        public ISkillResult SkillResult { get; set; }
    }
}
