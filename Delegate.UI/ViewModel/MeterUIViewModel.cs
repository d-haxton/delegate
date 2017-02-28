using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Delegate.Meter.interfaces;
using Delegate.UI.FodyPropertyChanged;
using Delegate.UI.interfaces;
using Delegate.UI.Model;
using Delegate.UI.View;
using GalaSoft.MvvmLight.Command;

namespace Delegate.UI.ViewModel
{
    public class MeterUIViewModel 
    {
        private readonly IMeterUiModel _model;
        private readonly ISettingsWrapper _wrapper;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MeterUIViewModel(IMeterUiModel model, ISettingsWrapper wrapper)
        {
            _model = model;
            _wrapper = wrapper;
            SettingsCommand = new RelayCommand(OpenSettingsWindow);

            UpdateAlphaColor();

            wrapper.SaveEvent += OnSaveEvent;
        }

        public ObservableCollection<IDelegateBreakdown> Breakdown => _model.Breakdown;
        public ObservableCollection<string> HistoryBreakdown => _model.HistoryBreakdown;
        public Color DataGridColor { get; set; }
        public ICommand SettingsCommand { get; }
        public int AlphaInt => _wrapper.OpacityValue / 100;

        public int SelectedHistory
        {
            get
            {
                return _model.SelectedHistory;
            }
            set
            {
                if (value != -1)
                {
                    _model.SelectedHistory = value;
                }
            }
        }


        public string TitleText => _model.TitleText;

        private void UpdateAlphaColor()
        {
            var alpha = _wrapper.OpacityValue;
            var color = new Color {A = (byte) alpha, R = 0, B = 0, G = 0};
            DataGridColor = color;
            //RaisePropertyChanged(() => DataGridColor);
            //RaisePropertyChanged(() => AlphaInt);
        }

        private void OnSaveEvent(object sender, EventArgs e)
        {
            UpdateAlphaColor();
        }

        private void OpenSettingsWindow()
        {
            var settingsWindow = new SettingsViewController();
            settingsWindow.Show();
        }

        //protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "UpdatedCounter")
        //    {
        //        RaisePropertyChanged("Breakdown");
        //        RaisePropertyChanged(() => Breakdown);
        //    }
        //    else
        //    {
        //        RaisePropertyChanged(e.PropertyName);
        //    }
        //}
    }
}