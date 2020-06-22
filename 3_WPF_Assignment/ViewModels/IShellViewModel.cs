namespace _3_WPF_Assignment.ViewModels
{
    public interface IShellViewModel
    {
        object SelectedPrime { get; set; }
        ulong? Number { get; }
    }
}