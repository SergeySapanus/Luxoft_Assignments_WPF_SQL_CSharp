using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace _3_WPF_Assignment.Models
{
    public interface IPrimesModel
    {
        ReadOnlyObservableCollection<ulong> Primes { get; }

        string GetPrimesUpCaption(ulong? number);

        void AddPrime(ulong prime);
        void ClearPrimes();

        IEnumerable<ulong> GetPrimesByEratosthenesSieve(ulong sqrtLimit1, ulong limit);
    }
}