using Delegate.UI.interfaces;

namespace Delegate.UI.ViewModel
{
    public class SettingsViewModel
    {
        private readonly ISettingsWrapper _wrapper;

        public SettingsViewModel(ISettingsWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public bool DataGridDamage
        {
            get
            {
                return _wrapper.DataGridDamage;
            }
            set
            {
                _wrapper.DataGridDamage = value;
            }
        }

        public bool DataGridDamagePercent
        {
            get
            {
                return _wrapper.DataGridDamagePercent;
            }
            set
            {
                _wrapper.DataGridDamagePercent = value;
            }
        }

        public bool DataGridPlayer
        {
            get
            {
                return _wrapper.DataGridPlayer;
            }
            set
            {
                _wrapper.DataGridPlayer = value;
            }
        }

        public bool DataGridClass
        {
            get
            {
                return _wrapper.DataGridClass;
            }
            set
            {
                _wrapper.DataGridClass = value;
            }
        }

        public bool DataGridTarget
        {
            get
            {
                return _wrapper.DataGridTarget;
            }
            set
            {
                _wrapper.DataGridTarget = value;
            }
        }

        public bool DataGridDps
        {
            get
            {
                return _wrapper.DataGridDps;
            }
            set
            {
                _wrapper.DataGridDps = value;
            }
        }

        public bool DataGridCritPercent
        {
            get
            {
                return _wrapper.DataGridCritPercent;
            }
            set
            {
                _wrapper.DataGridCritPercent = value;
            }
        }

        public bool DataGridHealing
        {
            get
            {
                return _wrapper.DataGridHealing;
            }
            set
            {
                _wrapper.DataGridHealing = value;
            }
        }

        public bool DataGridHps
        {
            get
            {
                return _wrapper.DataGridHps;
            }
            set
            {
                _wrapper.DataGridHps = value;
            }
        }

        public bool DataGridLastActive
        {
            get
            {
                return _wrapper.DataGridLastActive;
            }
            set
            {
                _wrapper.DataGridLastActive = value;
            }
        }

        public int OpacityValue
        {
            get
            {
                return _wrapper.OpacityValue;
            }
            set
            {
                _wrapper.OpacityValue = value;
            }
        }
    }
}