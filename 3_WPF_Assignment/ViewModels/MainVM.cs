using System;
using System.ComponentModel;
using _3_WPF_Assignment.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class MainVM : BindableBase
    {
        #region Fields

        private readonly IMessageBoxService _messageBoxService;
        private object _selectedPrime;

        #endregion Fields
        #region Properties

        public PrimesVM PrimesVM { get; }
        public InputVM InputVM { get; }

        public object SelectedPrime
        {
            get => _selectedPrime;
            set => SetProperty(ref _selectedPrime, value);
        }

        #endregion Properties

        public MainVM(IMessageBoxService messageBoxService, IEventAggregator aggregator)
        {
            _messageBoxService = messageBoxService ?? throw new ArgumentException(nameof(messageBoxService));

            InputVM = new InputVM(aggregator);
            PrimesVM = new PrimesVM(aggregator);

            OKCommand = new DelegateCommand<object>(OKCommand_Execute, OKCommand_CanExecute);
            OKCommand.ObservesProperty(() => SelectedPrime);
            OKCommand.ObservesProperty(() => PrimesVM.Number);
        }

        public MainVM() : this(new MessageBoxService(), new EventAggregator())
        {
            //throw new System.NotImplementedException();
        }

        #region Commands

        public DelegateCommand<object> OKCommand { get; }

        private bool OKCommand_CanExecute(object sender)
        {
            return PrimesVM.Number.HasValue && SelectedPrime != null;
        }

        private void OKCommand_Execute(object sender)
        {
            _messageBoxService.ShowMessage($"You have chosen {PrimesVM.Number} and {SelectedPrime}");
        }

        #endregion Commands
    }
}