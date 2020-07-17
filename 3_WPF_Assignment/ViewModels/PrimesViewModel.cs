using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using _3_WPF_Assignment.Events;
using _3_WPF_Assignment.Models;
using MathHelpers;
using Prism.Events;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class PrimesViewModel : BindableBase, IPrimesViewModel
    {
        #region Fields

        private readonly IPrimesModel _primesModel;

        private ulong? _number;

        #endregion Fields
        #region Properties

        public ulong? Number
        {
            get => _number;
            set
            {
                SetProperty(ref _number, value);
                RaisePropertyChanged(nameof(PrimesUpCaption));
                CalculatePrimes();
            }
        }

        public string PrimesUpCaption => _primesModel.GetPrimesUpCaption(_number);

        public ReadOnlyObservableCollection<ulong> Primes => _primesModel.Primes;

        #endregion Properties

        public PrimesViewModel(IEventAggregator aggregator, IPrimesModel primesModel)
        {
            _primesModel = primesModel;

            aggregator.GetEvent<NumberEvent>().Subscribe(number => Number = number);
        }

        public void CalculatePrimes()
        {
            if (!Number.HasValue)
                return;

            var currentDispatcher = Dispatcher.CurrentDispatcher;

            Task.Factory.StartNew(() =>
            {
                var primesModel = (PrimesModel)_primesModel;

                currentDispatcher.Invoke(() =>
                {
                    primesModel.ClearPrimes();
                });

                foreach (var item in primesModel.GetPrimesByEratosthenesSieve(Number.Value, WPFAssignmentHelper.Power2(Number.Value)))
                {
                    currentDispatcher.InvokeAsync(() =>
                    {
                        primesModel.AddPrime(item);
                    });
                }
            });
        }
    }
}