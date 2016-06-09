using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CommonData
{
    public class HeartDataBackground
    {
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
            }
        }
    }
}
