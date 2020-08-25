using System.Collections.Specialized;

namespace _3_WPF_Assignment.ViewModels
{
    public interface IShellViewModel
    {
        event NotifyCollectionChangedEventHandler PrimesCollectionChanged;
        object SelectedPrime { get; set; }
        ulong? Number { get; }
    }
}