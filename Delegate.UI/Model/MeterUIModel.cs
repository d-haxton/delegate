using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Delegate.Meter.events;
using Delegate.Meter.interfaces;
using Delegate.UI.FodyPropertyChanged;
using Delegate.UI.interfaces;
using Delegate.UI.View;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;

namespace Delegate.UI.Model
{
    public class MeterUiModel : BaseNotifyingModel, IMeterUiModel
    {
        private readonly IDelegateCombatControl _combatControl;

        public MeterUiModel(IDelegateMeter meter, IDelegateCombatControl combatControl)
        {
            _combatControl = combatControl;
            meter.CombatUpdatedEvent += OnCombatUpdated;

            Breakdown = new ObservableCollection<IDelegateBreakdown>();
            HistoryBreakdown = new ObservableCollection<string>();
            HistoryList = new Dictionary<string, ObservableCollection<IDelegateBreakdown>>();

            UpdatedCounter = 0;
            TitleText = "Delegate";

            Messenger.Default.Register<SelectionChangedEventArgs>(this, UpdatedIndex);
            Messenger.Default.Register<DataGrid>(this, OnDataGridClick);

            // var breakdown = new DelegateBreakdown {Source = "Me you fuck"};
            // Breakdown.Add(breakdown);
        }

        private Dictionary<string, ObservableCollection<IDelegateBreakdown>> HistoryList { get; }
        private bool Paused { get; set; }
        public ObservableCollection<IDelegateBreakdown> Breakdown { get; set; }
        public ObservableCollection<string> HistoryBreakdown { get; set; }
        public int SelectedHistory { get; set; }
        public int UpdatedCounter { get; set; }
        public string TitleText { get; set; }

        private void OnDataGridClick(DataGrid args)
        {
            var breakdown = args.CurrentItem as IDelegateBreakdown;
            var detailed = new DetailedView(breakdown);
            detailed.Show();
        }

        private void UpdatedIndex(SelectionChangedEventArgs args)
        {
            Paused = SelectedHistory != 0;

            var localIndex = SelectedHistory;

            if (localIndex == -1)
            {
                localIndex = 0;
            }

            if (localIndex >= HistoryBreakdown.Count)
            {
                return;
            }

            var stringOfBreakdown = HistoryBreakdown[localIndex];
            var success = HistoryList.TryGetValue(stringOfBreakdown, out ObservableCollection<IDelegateBreakdown> localHistory);

            if (success == false)
            {
                return;
            }

            TitleText = "Delegate - PAUSED";
            Breakdown.Clear();
            foreach (var breakdowns in localHistory)
            {
                Breakdown.Add(breakdowns);
            }
            UpdatedCounter++;
        }

        private string MostCommonString(IEnumerable<string> listOfS)
        {
            var groupsWithCounts = from s in listOfS group s by s into g select new {Item = g.Key, Count = g.Count()};

            var groupsSorted = groupsWithCounts.OrderByDescending(g => g.Count);
            return groupsSorted.First().Item;
        }

        private string GetLast(List<string> s)
        {
            s.Sort();
            return s.Last();
        }

        private void BuildHistory()
        {
            HistoryList.Clear();
            HistoryBreakdown.Clear();

            var listOfTargets = new List<string>();
            var listOfTimes = new List<string>();

            foreach (var history in _combatControl.CombatHistory)
            {
                listOfTargets.AddRange(history.Select(breakdown => breakdown.Target));

                listOfTimes.AddRange(history.Select(breakdown => breakdown.Time.ToString(CultureInfo.CurrentCulture)));

                var titleForHistory = $"{GetLast(listOfTimes)} {MostCommonString(listOfTargets)}";
                HistoryBreakdown.Insert(0, titleForHistory);
                HistoryList[titleForHistory] = history;

                listOfTargets.Clear();
            }
        }

        private void OnCombatUpdated(object sender, CombatUpdatedArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                BuildHistory();
                if (Paused)
                {
                    TitleText = "Delegate - PAUSED";
                    return;
                }

                TitleText = "Delegate - Connected";
                Breakdown.Clear();
                foreach (var breakdowns in _combatControl.CombatHistory[_combatControl.CombatHistory.Count - 1])
                {
                    Breakdown.Add(breakdowns);
                }
                UpdatedCounter++;
            });
        }
    }
}