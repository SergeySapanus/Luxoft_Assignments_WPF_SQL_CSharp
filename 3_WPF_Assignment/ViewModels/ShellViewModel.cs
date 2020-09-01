using System;
using System.Collections.Specialized;
using _3_WPF_Assignment.Commands;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class ShellViewModel : BindableBase, IShellViewModel
    {
        #region Fields

        private object _selectedPrime;

        #endregion Fields
        #region Properties

        public IOKCommand OKCommand { get; set; }

        public IPrimesViewModel PrimesViewModel { get; }

        public IInputViewModel InputViewModel { get; }

        #endregion Properties

        public ShellViewModel(IPrimesViewModel primesViewModel, IInputViewModel inputViewModel)
        {
            PrimesViewModel = primesViewModel;
            InputViewModel = inputViewModel;
        }

        #region IShellViewModel

        public ulong? Number => PrimesViewModel.Number;

        public event EventHandler<NotifyCollectionChangedEventArgs> OnAddPrime
        {
            add => PrimesViewModel.OnAddPrime +=value;
            remove => PrimesViewModel.OnAddPrime -= value;
        }

        public object SelectedPrime
        {
            get => _selectedPrime;
            set => SetProperty(ref _selectedPrime, value);
        }

        #endregion IShellViewModel
    }
}