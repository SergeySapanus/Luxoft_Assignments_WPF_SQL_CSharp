using System.Windows;
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
            containerRegistry
                .RegisterSingleton<IMessageBoxService, MessageBoxService>()
                .RegisterSingleton<IEventAggregator, EventAggregator>()
                .RegisterSingleton<ShellViewModel, ShellViewModel>();

            containerRegistry
                .Register<PrimesViewModel, PrimesViewModel>()
                .Register<InputViewModel, InputViewModel>();

            var container = Container.GetContainer();

            var shellViewModel = container.Resolve<ShellViewModel>();
            container.InjectPropertiesAndFields(shellViewModel,new[]
            {
                nameof(ShellViewModel.PrimesViewModel), 
                nameof(ShellViewModel.InputViewModel)
            });
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}
