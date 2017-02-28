using System.Collections.ObjectModel;
using System.Windows.Media;
using Delegate.Meter.interfaces;

namespace Delegate.UI.interfaces
{
    public interface IMeterUiModel
    {
        ObservableCollection<IDelegateBreakdown> Breakdown { get; }
        int UpdatedCounter { get; }
        string TitleText { get; set; }
        ObservableCollection<string> HistoryBreakdown { get; }
        int SelectedHistory { get; set; }
    }
}
