using System;
using System.Collections.ObjectModel;
using MathHelpers;
using Prism.Mvvm;

namespace _3_WPF_Assignment.Models
{
    public class PrimesModel : BindableBase, IDisposable
    {
        #region Fields

        private readonly ObservableCollection<ulong> _primes;
        private readonly ReadOnlyObservableCollection<ulong> _primesReadOnly;

        #endregion Fields

        public PrimesModel()
        {
            _primes = new ObservableCollection<ulong>();
            _primesReadOnly = new ReadOnlyObservableCollection<ulong>(_primes);
        }

        public ReadOnlyObservableCollection<ulong> GetPrimes(ulong? input)
        {
            _primes.Clear();

            if (!input.HasValue)
            {
                return _primesReadOnly;
            }

            var primes = WPFAssignmentHelper.GetPrimesByEratosthenesSieve(input.Value, WPFAssignmentHelper.Power2(input.Value));
            _primes.AddRange(primes);

            return _primesReadOnly;
        }

        public string GetPrimesUpCaption(ulong? input)
        {
            if (!input.HasValue)
                return "Enter number, please";

            return $"Primes up to{Environment.NewLine}{WPFAssignmentHelper.Power2(input.Value)}:";
        }

        #region IDisposable

        public void Dispose()
        {
        }

        #endregion IDisposable
    }
}