using System;
using Delegate.UI.interfaces;
using Delegate.UI.Properties;


namespace Delegate.UI.dependencies
{
    public class SettingsWrapper : ISettingsWrapper
    {
        public bool DataGridDamage
        {
            get
            {
                return Settings.Default.DGDamage;
            }
            set
            {
                Settings.Default.DGDamage = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridDamagePercent
        {
            get
            {
                return Settings.Default.DGDamagePercent;
            }
            set
            {
                Settings.Default.DGDamagePercent = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridPlayer
        {
            get
            {
                return Settings.Default.DGPlayer;
            }
            set
            {
                Settings.Default.DGPlayer = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridClass
        {
            get
            {
                return Settings.Default.DGClass;
            }
            set
            {
                Settings.Default.DGClass = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridTarget
        {
            get
            {
                return Settings.Default.DGTarget;
            }
            set
            {
                Settings.Default.DGTarget = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridDps
        {
            get
            {
                return Settings.Default.DGDps;
            }
            set
            {
                Settings.Default.DGDps = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridCritPercent
        {
            get
            {
                return Settings.Default.DGCritPercent;
            }
            set
            {
                Settings.Default.DGCritPercent = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridHealing
        {
            get
            {
                return Settings.Default.DGHealing;
            }
            set
            {
                Settings.Default.DGHealing = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridHps
        {
            get
            {
                return Settings.Default.DGHps;
            }
            set
            {
                Settings.Default.DGHps = value;
                SaveAndFireEvent();
            }
        }

        public bool DataGridLastActive
        {
            get
            {
                return Settings.Default.DGLastActive;
            }
            set
            {
                Settings.Default.DGLastActive = value;
                SaveAndFireEvent();
            }
        }

        public int OpacityValue
        {
            get
            {
                return Settings.Default.OpacityValue;
            }
            set
            {
                Settings.Default.OpacityValue = value;
                SaveAndFireEvent();
            }
        }

        public event EventHandler SaveEvent = delegate { };

        private void SaveAndFireEvent()
        {
            Settings.Default.Save();
            SaveEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}