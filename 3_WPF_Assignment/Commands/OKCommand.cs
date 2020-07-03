using System;
using System.Linq.Expressions;
using _3_WPF_Assignment.Services;
using _3_WPF_Assignment.ViewModels;
using Prism.Commands;

namespace _3_WPF_Assignment.Commands
{
    public class OKCommand : DelegateCommand<object>, IOKCommand
    {
        #region Fields

        private static IMessageBoxService _messageBoxService;
        private static IShellViewModel _shellViewModel;

        #endregion Fields

        public OKCommand(IMessageBoxService messageBoxService, IShellViewModel shellViewModel)
            : base(OKCommand_Execute, OKCommand_CanExecute)
        {
            _messageBoxService = messageBoxService;
            _shellViewModel = shellViewModel;

            ObservesProperty<object>(_shellViewModel, nameof(_shellViewModel.SelectedPrime));
            ObservesProperty<ulong?>(_shellViewModel, nameof(_shellViewModel.Number));
        }

        private void ObservesProperty<T>(IShellViewModel shellViewModel, string propertyName)
        {
            var memberExpression = Expression.PropertyOrField(Expression.Constant(shellViewModel), propertyName);
            var expression = Expression.Lambda<Func<T>>(memberExpression);
            ObservesProperty(expression);
        }

        private static bool OKCommand_CanExecute(object sender)
        {
            return _shellViewModel.Number.HasValue && _shellViewModel.SelectedPrime != null;
        }

        private static void OKCommand_Execute(object sender)
        {
            _messageBoxService.ShowMessage($"You have chosen {_shellViewModel.Number} and {_shellViewModel.SelectedPrime}");
        }
    }
}