using _3_WPF_Assignment.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class MainVM : BindableBase
    {
        #region Fields

        private readonly IMessageBoxService _messageBoxService;

        private string _input;
        private object _selectedPrime;

        #endregion Fields
        #region Properties

        public PrimesVM PrimesVM { get; }

        public string Input
        {
            get => _input;
            set
            {
                SetProperty(ref _input, value);
                SetNumber();
            }
        }

        public object SelectedPrime
        {
            get => _selectedPrime;
            set => SetProperty(ref _selectedPrime, value);
        }

        public DelegateCommand<object> OKCommand { get; }

        #endregion Properties

        public MainVM(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;

            PrimesVM = new PrimesVM();

            OKCommand = new DelegateCommand<object>(OKCommand_Execute, OKCommand_CanExecute);
            OKCommand.ObservesProperty(() => SelectedPrime);
            OKCommand.ObservesProperty(() => PrimesVM.Number);
        }

        public MainVM() : this(new MessageBoxService())
        {
            //throw new System.NotImplementedException();
        }

        private bool OKCommand_CanExecute(object sender)
        {
            return PrimesVM.Number.HasValue && SelectedPrime != null;
        }

        private void OKCommand_Execute(object sender)
        {
            _messageBoxService.ShowMessage($"You have chosen {PrimesVM.Number} and {SelectedPrime}");
        }

        private void SetNumber()
        {
            if (string.IsNullOrWhiteSpace(_input) || !ulong.TryParse(_input, out var result))
            {
                PrimesVM.Number = null;
            }
            else
            {
                PrimesVM.Number = result;
            }
        }
    }
}