using System.ComponentModel;
using GalaSoft.MvvmLight;
using PropertyChanged;

namespace Delegate.UI.FodyPropertyChanged
{
    [ImplementPropertyChanged]
    public class TypedViewModelBase<T> : ViewModelBase where T : BaseNotifyingModel
    {
        private T _model;

        public TypedViewModelBase()
        {
        }

        public TypedViewModelBase(T model)
        {
            Model = model;
        }

        public T Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (value != _model)
                {
                    if (_model != null)
                    {
                        _model.PropertyChanged -= ModelPropertyChanged;
                    }
                    _model = value;
                    if (_model != null)
                    {
                        _model.PropertyChanged += ModelPropertyChanged;
                    }
                }
            }
        }

        protected virtual void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }
    }
}