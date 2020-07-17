using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AddPrime(ulong prime)
        {
            _primes.Add(prime);
        }

        public void ClearPrimes()
        {
            _primes.Clear();
        }

        public async Task<IEnumerable<ulong>> GetPrimesByEratosthenesSieveAsync(ulong sqrtLimit, ulong limit)
        {
            return await Task.Factory.StartNew(() => GetPrimesByEratosthenesSieve(sqrtLimit, limit)).ConfigureAwait(false);
        }

        public IEnumerable<ulong> GetPrimesByEratosthenesSieve(ulong sqrtLimit1, ulong limit)
        {
            foreach (var prime in WPFAssignmentHelper.GetPrimesByEratosthenesSieve(sqrtLimit1, limit))
            {
                yield return prime;
            }
        }
    }
}