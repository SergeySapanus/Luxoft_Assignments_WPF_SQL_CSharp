using System;
using System.ComponentModel;
using _3_WPF_Assignment.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        #region Fields

        private readonly IMessageBoxService _messageBoxService;
        private object _selectedPrime;

        #endregion Fields
        #region Properties

        public PrimesViewModel PrimesViewModel { get; }
        public InputViewModel InputViewModel { get; }

        public object SelectedPrime
        {
            get => _selectedPrime;
            set => SetProperty(ref _selectedPrime, value);
        }

        #endregion Properties

        public ShellViewModel(IMessageBoxService messageBoxService, IEventAggregator aggregator)
        {
            _messageBoxService = messageBoxService ?? throw new ArgumentException(nameof(messageBoxService));

            //InputViewModel = new InputViewModel(aggregator);
            //PrimesViewModel = new PrimesViewModel(aggregator);

            OKCommand = new DelegateCommand<object>(OKCommand_Execute, OKCommand_CanExecute);
            OKCommand.ObservesProperty(() => SelectedPrime);
            OKCommand.ObservesProperty(() => PrimesViewModel.Number);
        }

        #region Commands

        public DelegateCommand<object> OKCommand { get; }

        private bool OKCommand_CanExecute(object sender)
        {
            return PrimesViewModel.Number.HasValue && SelectedPrime != null;
        }

        private void OKCommand_Execute(object sender)
        {
            _messageBoxService.ShowMessage($"You have chosen {PrimesViewModel.Number} and {SelectedPrime}");
        }

        #endregion Commands
    }
}