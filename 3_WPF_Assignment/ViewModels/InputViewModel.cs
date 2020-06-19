using System;
using System.Windows.Media;
using _3_WPF_Assignment.Events;
using Prism.Events;
using Prism.Mvvm;

namespace _3_WPF_Assignment.ViewModels
{
    public class InputViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator _aggregator;

        private ulong? _number;
        private string _input;

        #endregion Fields
        #region Properties

        public string Input
        {
            get => _input;
            set
            {
                SetProperty(ref _input, value);
                SetNumber();
                RaisePropertyChanged(nameof(Foreground));
                RaisePropertyChanged(nameof(Background));
            }
        }

        public Brush Foreground
        {
            get
            {
                if (string.IsNullOrEmpty(_input))
                    return Brushes.Blue;

                return Brushes.White;
            }
        }

        public Brush Background
        {
            get
            {
                if (_number.HasValue)
                    return Brushes.Green;

                return !string.IsNullOrEmpty(_input) ? Brushes.Maroon : Brushes.White;
            }
        }

        public ulong? Number
        {
            get => _number;
            set
            {
                if (SetProperty(ref _number, value))
                {
                    _aggregator.GetEvent<NumberEvent>().Publish(_number);
                }
            }
        }

        #endregion Properties

        public InputViewModel(IEventAggregator aggregator)
        {
            _aggregator = aggregator ?? throw new ArgumentException(nameof(aggregator));
        }

        private void SetNumber()
        {
            if (string.IsNullOrWhiteSpace(_input) || !ulong.TryParse(_input, out var result))
            {
                Number = null;
            }
            else
            {
                Number = result;
            }
        }
    }
}