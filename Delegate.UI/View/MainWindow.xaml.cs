using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;

namespace Delegate.UI.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AllowsTransparency = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send(e);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Messenger.Default.Send(e);
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGrid;
            if (row != null)
            {
                Messenger.Default.Send(row);
            }
        }

        private void DataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}