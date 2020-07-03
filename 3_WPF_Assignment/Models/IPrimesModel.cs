using System.Collections.ObjectModel;

namespace _3_WPF_Assignment.Models
{
    public interface IPrimesModel
    {
        ReadOnlyObservableCollection<ulong> Primes { get; }

        string GetPrimesUpCaption(ulong? number);

        void CalculatePrimes(ulong? number);
    }
}