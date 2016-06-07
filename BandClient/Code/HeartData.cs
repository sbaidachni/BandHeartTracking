using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandClient.Code
{
    public class HeartData: INotifyPropertyChanged
    {
        public ObservableCollection<int> Rates { get; set; }

        public HeartData()
        {
            Rates = new ObservableCollection<int>();
            for (int i=50;i<=200;i++)
            {
                Rates.Add(i);
            }
        }

        public int CurrentRateIndex { get; set; }

        private int _currentRate;

        private int _minRate;

        private int _maxRate;

        public int CurrentRate
        {
            get { return _currentRate; }
            set
            {
                _currentRate = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurrentRate"));
            }
        }

        public int MinRate
        {
            get { return _minRate; }
            set
            {
                _minRate = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MinRate"));
            }
        }

        public int MaxRate
        {
            get { return _maxRate; }
            set
            {
                _maxRate = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MaxRate"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
