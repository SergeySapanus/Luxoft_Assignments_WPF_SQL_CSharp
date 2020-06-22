using System.Windows;
using _3_WPF_Assignment.Commands;
using _3_WPF_Assignment.Services;
using _3_WPF_Assignment.ViewModels;
using _3_WPF_Assignment.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;

namespace _3_WPF_Assignment
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var container = Container.GetContainer();

            container.Register<IMessageBoxService, MessageBoxService>(Reuse.Singleton);
            container.Register<IEventAggregator, EventAggregator>(Reuse.Singleton);
            container.Register<ShellViewModel, ShellViewModel>(Reuse.Singleton);

            container.RegisterMapping<IShellViewModel, ShellViewModel>();

            container.Register<PrimesViewModel, PrimesViewModel>();
            container.Register<InputViewModel, InputViewModel>();

            container.Register<OKCommand, OKCommand>();

            var shellViewModel = container.Resolve<ShellViewModel>();
            container.InjectPropertiesAndFields(shellViewModel, new[]
            {
                nameof(ShellViewModel.PrimesViewModel),
                nameof(ShellViewModel.InputViewModel),
                nameof(ShellViewModel.OKCommand)
            });
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}
