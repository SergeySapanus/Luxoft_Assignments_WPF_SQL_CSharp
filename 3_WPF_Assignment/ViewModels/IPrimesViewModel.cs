using System;
using System.Collections.Specialized;

namespace _3_WPF_Assignment.ViewModels
{
    public interface IPrimesViewModel
    {
        event EventHandler<NotifyCollectionChangedEventArgs> OnAddPrime;
        ulong? Number { get; }
    }
}