using System;
using System.Collections.Specialized;
using System.Windows;
using _3_WPF_Assignment.ViewModels;

namespace _3_WPF_Assignment.Views
{
    public partial class Shell : Window
    {
        public IShellViewModel ShellDataContext => DataContext as IShellViewModel;

        public Shell()
        {
            InitializeComponent();

            tbNumber.Focus();

            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            Dispatcher.ShutdownFinished += Dispatcher_ShutdownFinished;

            var dataContext = ShellDataContext;
            if (dataContext != null)
            {
                dataContext.OnAddPrime += DataContext_OnAddPrime;
            }
        }

        private void DataContext_OnAddPrime(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    var item = e.NewItems[0];
                    if (item != null)
                    {
                        lbPrimes.ScrollIntoView(item);
                    }
                }
                    break;
            }
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            var dataContext = ShellDataContext;
            if (dataContext != null)
            {
                dataContext.OnAddPrime -= DataContext_OnAddPrime;
            }

            var disposable = DataContext as IDisposable;
            disposable?.Dispose();
        }

        private void Dispatcher_ShutdownFinished(object sender, EventArgs e)
        {
            Dispatcher.ShutdownStarted -= Dispatcher_ShutdownStarted;
            Dispatcher.ShutdownFinished -= Dispatcher_ShutdownFinished;
        }
    }
}
