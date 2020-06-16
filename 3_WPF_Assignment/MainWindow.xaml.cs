using System;
using System.Windows;

namespace _3_WPF_Assignment
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
