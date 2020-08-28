using System;
using System.Collections.Specialized;

namespace _3_WPF_Assignment.ViewModels
{
    public interface IShellViewModel
    {
        event EventHandler<NotifyCollectionChangedEventArgs> OnAddPrime;
        object SelectedPrime { get; set; }
        ulong? Number { get; }
    }
}