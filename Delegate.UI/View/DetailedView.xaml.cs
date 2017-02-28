using System.Collections.Generic;
using Delegate.Meter.interfaces;

namespace Delegate.UI.View
{
    /// <summary>
    ///     Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView
    {
        public DetailedView(IDelegateBreakdown breakdown)
        {
            Breakdown = new List<ISkillResult>();
            foreach (var history in breakdown.History)
            {
                Breakdown.Add(history);
            }
            InitializeComponent();
        }

        public List<ISkillResult> Breakdown { get; set; }
    }
}