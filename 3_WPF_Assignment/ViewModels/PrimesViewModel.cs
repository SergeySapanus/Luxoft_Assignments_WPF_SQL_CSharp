using System.Collections.ObjectModel;
using _3_WPF_Assignment.Events;
using _3_WPF_Assignment.Models;
using Prism.Events;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class PrimesViewModel : BindableBase
    {
        #region Fields

        private readonly PrimesModel _primesModel;

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

        public PrimesViewModel(IEventAggregator aggregator, PrimesModel primesModel)
        {
            _primesModel = primesModel;

            aggregator.GetEvent<NumberEvent>().Subscribe(number => Number = number);
        }

        public void CalculatePrimes() => _primesModel.CalculatePrimes(_number);
    }
}