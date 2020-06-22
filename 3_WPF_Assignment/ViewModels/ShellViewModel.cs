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

        public OKCommand OKCommand { get; set; }

        public PrimesViewModel PrimesViewModel { get; set; }
        public InputViewModel InputViewModel { get; set; }

        #endregion Properties

        #region IShellViewModel

        public ulong? Number => PrimesViewModel.Number;

        public object SelectedPrime
        {
            get => _selectedPrime;
            set => SetProperty(ref _selectedPrime, value);
        }

        #endregion IShellViewModel
    }
}