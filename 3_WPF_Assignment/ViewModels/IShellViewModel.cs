using _3_WPF_Assignment.Commands;

namespace _3_WPF_Assignment.ViewModels
{
    public interface IShellViewModel
    {
        IOKCommand OKCommand { get; set; }

        IPrimesViewModel PrimesViewModel { get; set; }
        IInputViewModel InputViewModel { get; set; }

        object SelectedPrime { get; set; }
        ulong? Number { get; }
    }
}