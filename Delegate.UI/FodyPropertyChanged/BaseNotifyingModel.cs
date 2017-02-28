using System.ComponentModel;
using GalaSoft.MvvmLight.Threading;
using PropertyChanged;

namespace Delegate.UI.FodyPropertyChanged
{
    [ImplementPropertyChanged]
    public class BaseNotifyingModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(
                    () => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}