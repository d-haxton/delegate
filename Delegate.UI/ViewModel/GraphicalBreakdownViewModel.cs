using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Delegate.Meter.interfaces;
using Delegate.UI.Annotations;
using LiveCharts;

namespace Delegate.UI.ViewModel
{
    public interface IGraphicalBreakdownViewModel : INotifyPropertyChanged
    {
        SeriesCollection SeriesCollection { get; set; }
    }

    public class GraphicalBreakdownViewModel : IGraphicalBreakdownViewModel
    {
        private readonly IDelegateBreakdown _breakdown;
        public SeriesCollection SeriesCollection { get; set; }

        public GraphicalBreakdownViewModel(IDelegateBreakdown breakdown)
        {
            _breakdown = breakdown;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
