using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using _3_WPF_Assignment.Events;
using _3_WPF_Assignment.Models;
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
            var currentDispatcher = Dispatcher.CurrentDispatcher;

            Task.Factory.StartNew(() =>
            {
                for (var i = 1ul; i <= 1000ul; i++)
                {
                    var local = i;

                    currentDispatcher.InvokeAsync(() =>
                    {
                        Number = local;
                    });

                    Thread.Sleep(1000);
                }
            });

            //_primesModel.CalculatePrimes(_number);
        }
    }
}