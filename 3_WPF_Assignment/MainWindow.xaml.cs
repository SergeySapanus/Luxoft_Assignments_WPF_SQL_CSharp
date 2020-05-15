using System.Windows;
using _3_WPF_Assignment.Model;

namespace _3_WPF_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NumberModel _numberModel = new NumberModel();

        public MainWindow()
        {
            InitializeComponent();

            tbNumber.Focus();
        }
    }
}
