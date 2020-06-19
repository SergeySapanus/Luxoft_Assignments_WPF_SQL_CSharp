using System.Windows;
using _3_WPF_Assignment.Services;
using _3_WPF_Assignment.ViewModels;
using _3_WPF_Assignment.Views;
using Prism.Events;
using Prism.Ioc;

namespace _3_WPF_Assignment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IMessageBoxService, MessageBoxService>()
                .RegisterSingleton<IEventAggregator, EventAggregator>();

            containerRegistry
                .Register<PrimesViewModel, PrimesViewModel>()
                .Register<InputViewModel, InputViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}
