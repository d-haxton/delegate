﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.events;

namespace Delegate.Meter.interfaces
{
    public interface IDelegateMeter
    {
        event EventHandler<CombatUpdatedArgs> CombatUpdatedEvent;
    }
}
