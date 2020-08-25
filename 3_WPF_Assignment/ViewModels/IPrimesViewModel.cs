using System.Collections.Specialized;

namespace _3_WPF_Assignment.ViewModels
{
    public interface IPrimesViewModel
    {
        event NotifyCollectionChangedEventHandler PrimesCollectionChanged;
        ulong? Number { get; }
    }
}