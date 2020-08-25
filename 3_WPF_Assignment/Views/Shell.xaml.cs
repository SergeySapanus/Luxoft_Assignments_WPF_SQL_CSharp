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
                dataContext.PrimesCollectionChanged += DataContext_PrimesCollectionChanged;
            }
        }

        private void DataContext_PrimesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    {
                        var item = lbPrimes.Items.GetItemAt(e.NewStartingIndex);
                        lbPrimes.ScrollIntoView(item);
                    }
                    break;
            }
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            var dataContext = ShellDataContext;
            if (dataContext != null)
            {
                dataContext.PrimesCollectionChanged -= DataContext_PrimesCollectionChanged;
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
