using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Delegate.UI.interfaces
{
    public interface ISettingsWrapper
    {
        bool DataGridDamage { get; set; }
        bool DataGridDamagePercent { get; set; }
        bool DataGridPlayer { get; set; }
        bool DataGridClass { get; set; }
        bool DataGridTarget { get; set; }
        bool DataGridDps { get; set; }
        bool DataGridCritPercent { get; set; }
        bool DataGridHealing { get; set; }
        bool DataGridHps { get; set; }
        bool DataGridLastActive { get; set; }
        int OpacityValue { get; set; }
        event EventHandler SaveEvent;
    }
}
