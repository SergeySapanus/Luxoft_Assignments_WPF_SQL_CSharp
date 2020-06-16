using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using _3_WPF_Assignment.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class PrimesViewModel : BindableBase, IDisposable
    {
        #region Fields

        private PrimesModel _primesModel = new PrimesModel();
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
                RaisePropertyChanged(nameof(Primes));
            }
        }

        public string PrimesUpCaption => _primesModel.GetPrimesUpCaption(_number);

        public ReadOnlyObservableCollection<ulong> Primes => _primesModel.GetPrimes(_number);

        #endregion Properties

        public PrimesViewModel()
        {
            OKCommand = new DelegateCommand<string>(x =>
            {

            });

            _primesModel.PropertyChanged += PrimesModel_PropertyChanged;

            ((INotifyCollectionChanged)Primes).CollectionChanged += PrimesViewModel_CollectionChanged;
        }

        private void PrimesViewModel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        public DelegateCommand<string> OKCommand { get; }

        private void PrimesModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        #region IDisposable

        public void Dispose()
        {
            if (_primesModel == null)
                return;

            ((INotifyCollectionChanged)Primes).CollectionChanged -= PrimesViewModel_CollectionChanged;
            _primesModel.PropertyChanged -= PrimesModel_PropertyChanged;

            _primesModel.Dispose();
            _primesModel = null;
        }

        #endregion IDisposable
    }
}