// <copyright file="ViewForUserControl.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2016 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using ReactiveUI;

namespace Delegate.UI.FodyPropertyChanged
{
    /// <summary>
    /// Base class for reactive UserControls. Adds a ViewModel DependencyProperty
    /// and allows for .OneWayBind() etc.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel class</typeparam>
    /// <typeparam name="TView">The UserControl class</typeparam>
    public class ViewForUserControl<TViewModel, TView> : UserControl, IViewFor<TViewModel>, INotifyPropertyChanged
        where TViewModel : class
    {
        private TViewModel _viewModel;

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = value as TViewModel; }
        }

        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}