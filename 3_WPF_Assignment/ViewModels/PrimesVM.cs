using System.Collections.ObjectModel;
using _3_WPF_Assignment.Models;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class PrimesVM : BindableBase
    {
        #region Fields

        private ulong? _number;

        private readonly PrimesModel _primesModel = new PrimesModel();

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

        public void CalculatePrimes() => _primesModel.CalculatePrimes(_number);
    }
}