using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _3_WPF_Assignment.Annotations;

namespace _3_WPF_Assignment.Model
{
    public class NumberModel : INotifyPropertyChanged
    {
        private int _input;

        public int Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        private IEnumerable<long> GetPrimes(long input)
        {
            return new[] { 2L, 3L, 5L, 7L, 11L };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}