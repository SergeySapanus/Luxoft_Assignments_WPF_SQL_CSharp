using System.Windows;

namespace _3_WPF_Assignment.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }
    }
}