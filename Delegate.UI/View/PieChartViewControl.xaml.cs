using System;
using System.Linq;
using System.Reactive.Linq;
using Delegate.Meter.interfaces;
using Delegate.UI.FodyPropertyChanged;
using Delegate.UI.ViewModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ReactiveUI;

namespace Delegate.UI.View
{
    public class PieChartBase : ViewForUserControl<GraphicalBreakdownViewModel, PieChartViewControl>
    {
    }

    /// <summary>
    ///     Interaction logic for PieChartViewControl.xaml
    /// </summary>
    public partial class PieChartViewControl
    {
        private readonly IDelegateCombatControl _combatControl;

        public PieChartViewControl(IDelegateCombatControl combatControl)
        {
            PieCollection = new SeriesCollection();
            _combatControl = combatControl;
            InitializeComponent();
            //this.OneWayBind(ViewModel, model => model.SeriesCollection, control => control._contentLoaded);
            _combatControl.CurrentCombat.ItemChanged.Select(x => ConvertBreakdownToSeriesCollection(x.Sender))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(UpdatePieSeries);
        }

        public SeriesCollection PieCollection { get; set; }

        private void UpdatePieSeries(PieSeries series)
        {
            var remove = PieCollection.FirstOrDefault(t => t.Title == series.Title);
            PieCollection.Remove(remove);

            PieCollection.Add(series);
        }
        private PieSeries ConvertBreakdownToSeriesCollection(IDelegateBreakdown breakdown)
        {
            var pieSeries = new PieSeries
            {
                Title = breakdown.Source,
                Values = new ChartValues<ObservableValue> {new ObservableValue(breakdown.Damage)},
                DataLabels = true
            };
            return pieSeries;
        }

        //public IGraphicalBreakdownViewModel ViewModel { get; set; }
    }
}