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

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly Dispatcher _currentDispatcher = Dispatcher.CurrentDispatcher;
        private Task _currentTask;

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

        private void CalculatePrimes()
        {
            if (!Number.HasValue)
                return;

            if (_currentTask != null)
            {
                _cancellationTokenSource.Cancel();
                _currentTask.Wait();
            }

            _currentTask = Task.Factory.StartNew(() =>
            {
                _currentDispatcher.InvokeAsync(() =>
                {
                    _primesModel.ClearPrimes();
                }).Wait();

                foreach (var item in _primesModel.GetPrimesByEratosthenesSieve(Number.Value, WPFAssignmentHelper.Power2(Number.Value)))
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    _currentDispatcher.InvokeAsync(() =>
                    {
                        _primesModel.AddPrime(item);
                    });
                }
            }, _cancellationTokenSource.Token).ContinueWith(task =>
            {
                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        {
                            _currentTask = null;
                        }
                        break;
                    case TaskStatus.Canceled:
                        {
                            _cancellationTokenSource = new CancellationTokenSource();
                        }
                        break;
                }
            });
        }
    }
}