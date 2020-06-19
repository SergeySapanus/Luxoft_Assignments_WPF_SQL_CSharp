using System;
using System.Windows;

namespace _3_WPF_Assignment.Views
{
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            tbNumber.Focus();

            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            Dispatcher.ShutdownFinished += Dispatcher_ShutdownFinished;
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
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
