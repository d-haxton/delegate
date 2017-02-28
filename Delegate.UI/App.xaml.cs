using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace Delegate.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.Initialize();
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
