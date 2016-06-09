using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CommonData
{
    public class HeartData : INotifyPropertyChanged
    {
        public ObservableCollection<int> Rates { get; set; }

        public HeartData()
        {
            Rates = new ObservableCollection<int>();
            for (int i = 50; i <= 200; i++)
            {
                Rates.Add(i);
            }
        }
        
        public bool IsStarted
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["IsStarted"] == null)
                    ApplicationData.Current.LocalSettings.Values["IsStarted"] = false;
                return (bool)ApplicationData.Current.LocalSettings.Values["IsStarted"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsStarted"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsStarted"));
            }
        }

        public Guid MyTileId
        {
            get
            {
                return new Guid("5E67A3C2-39D1-4F9B-BBFF-0D81CCE6D319");
            }
        }

        public bool IsMaxNotified
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["IsMaxNotified"] == null)
                    ApplicationData.Current.LocalSettings.Values["IsMaxNotified"] = false;
                return (bool)ApplicationData.Current.LocalSettings.Values["IsMaxNotified"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsMaxNotified"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsMaxNotified"));
            }
        }

        public bool IsMinNotified
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["IsMinNotified"] == null)
                    ApplicationData.Current.LocalSettings.Values["IsMinNotified"] = false;
                return (bool)ApplicationData.Current.LocalSettings.Values["IsMinNotified"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["IsMinNotified"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsMinNotified"));
            }
        }

        public int CurrentRateIndex { get; set; }

        public int CurrentRate
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["CurrentRate"] == null)
                    ApplicationData.Current.LocalSettings.Values["CurrentRate"] = 0;
                return (int)ApplicationData.Current.LocalSettings.Values["CurrentRate"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["CurrentRate"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurrentRate"));
            }
        }

        public int MinRate
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["MinRate"] == null)
                    ApplicationData.Current.LocalSettings.Values["MinRate"] = 0;
                return (int)ApplicationData.Current.LocalSettings.Values["MinRate"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["MinRate"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MinRate"));
            }
        }

        public int MaxRate
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["MaxRate"] == null)
                    ApplicationData.Current.LocalSettings.Values["MaxRate"] = 0;
                return (int)ApplicationData.Current.LocalSettings.Values["MaxRate"];
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["MaxRate"] = value;
                if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MaxRate"));
            }
        }

        public void RefreshAll()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MaxRate"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MinRate"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurrentRate"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
