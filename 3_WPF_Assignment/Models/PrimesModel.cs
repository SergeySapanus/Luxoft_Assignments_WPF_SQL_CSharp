using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using MathHelpers;

namespace _3_WPF_Assignment.Models
{
    public class PrimesModel : IPrimesModel
    {
        #region Fields

        private readonly ObservableCollection<ulong> _primes;
        private readonly ReadOnlyObservableCollection<ulong> _primesReadOnly;

        #endregion Fields

        #region Properties

        public ReadOnlyObservableCollection<ulong> Primes => _primesReadOnly;

        #endregion Properties

        public PrimesModel()
        {
            _primes = new ObservableCollection<ulong>();
            _primesReadOnly = new ReadOnlyObservableCollection<ulong>(_primes);
        }

        public string GetPrimesUpCaption(ulong? input)
        {
            if (!input.HasValue)
                return "Enter number, please";

            return $"Primes up to {WPFAssignmentHelper.Power2(input.Value)}:";
        }

        public async void CalculatePrimes(ulong? input)
        {
            _primes.Clear();

            if (!input.HasValue)
                return;

            foreach (var prime in await GetPrimesByEratosthenesSieveAsync(input.Value, WPFAssignmentHelper.Power2(input.Value)))
            {
                _primes.Add(prime);
            }
        }

        private async Task<IEnumerable<ulong>> GetPrimesByEratosthenesSieveAsync(ulong sqrtLimit, ulong limit)
        {
            return await Task.Factory.StartNew(() => GetPrimesByEratosthenesSieve(sqrtLimit, limit));
        }

        private IEnumerable<ulong> GetPrimesByEratosthenesSieve(ulong sqrtLimit1, ulong limit1)
        {
            foreach (var prime in WPFAssignmentHelper.GetPrimesByEratosthenesSieve(sqrtLimit1, limit1))
            {
                //Thread.Sleep(1000);
                yield return prime;
            }
        }
    }
}