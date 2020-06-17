using System.Collections.ObjectModel;
using MathHelpers;

namespace _3_WPF_Assignment.Models
{
    public class PrimesModel
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

        public void CalculatePrimes(ulong? input)
        {
            _primes.Clear();

            if (!input.HasValue)
                return;

            var primes = WPFAssignmentHelper.GetPrimesByEratosthenesSieve(input.Value, WPFAssignmentHelper.Power2(input.Value));
            _primes.AddRange(primes);
        }

        public string GetPrimesUpCaption(ulong? input)
        {
            if (!input.HasValue)
                return "Enter number, please";

            return $"Primes up to {WPFAssignmentHelper.Power2(input.Value)}:";
        }
    }
}