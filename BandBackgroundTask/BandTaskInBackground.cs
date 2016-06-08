using Microsoft.Band;
using Microsoft.Band.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Microsoft.Band.Sensors;
using Microsoft.Band.Notifications;
using Windows.Storage;

namespace BandBackgroundTask
{
    public sealed class BandTaskInBackground : IBackgroundTask
    {
        private BackgroundTaskDeferral Deferral;
        IBandClient bandClient;
        bool isStarted = false;
        Guid myTileId;
        bool isMaxNotified = false;
        bool isMinNotified = false;
        int maxRate = 0;
        int minRate = 0;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Deferral = taskInstance.GetDeferral();
            LoadValuesFromBackground();
            taskInstance.Canceled += OnCanceled;
            InitBand().Wait();
            
        }

        private void LoadValuesFromBackground()
        {
            isStarted = (bool)ApplicationData.Current.LocalSettings.Values["isStarted"];
            isMinNotified = (bool)ApplicationData.Current.LocalSettings.Values["isMinNotified"];
            isMaxNotified = (bool)ApplicationData.Current.LocalSettings.Values["isMaxNotified"];
            maxRate = (int)ApplicationData.Current.LocalSettings.Values["maxRate"];
            minRate = (int)ApplicationData.Current.LocalSettings.Values["minRate"];
        }

        private void SaveValuesForBackground()
        {
            ApplicationData.Current.LocalSettings.Values["isStarted"] = isStarted;
            ApplicationData.Current.LocalSettings.Values["isMaxNotified"] = isMaxNotified;
            ApplicationData.Current.LocalSettings.Values["isMinNotified"] = isMinNotified;
            ApplicationData.Current.LocalSettings.Values["maxRate"] = maxRate;
            ApplicationData.Current.LocalSettings.Values["minRate"] = minRate;
        }

        private async Task InitBand()
        {
            IBandInfo[] pairedBands;

            pairedBands = await BandClientManager.Instance.GetBandsAsync();

            try
            {
                if (bandClient == null)
                    bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

                bandClient.SensorManager.HeartRate.ReadingChanged += HeartRate_ReadingChanged;
                bool res = await bandClient.SensorManager.HeartRate.StartReadingsAsync();

                myTileId = new Guid("5E67A3C2-39D1-4F9B-BBFF-0D81CCE6D317");
            }
            catch (BandException ex)
            {
            }
        }

        private async void HeartRate_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
        {
            if (e.SensorReading.Quality == HeartRateQuality.Locked)
            {
                int currentRate = e.SensorReading.HeartRate;
                if (isStarted)
                {
                    if ((currentRate >= maxRate) && (!isMaxNotified))
                    {
                        isMaxNotified = true;
                        isMinNotified = false;
                        await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "You have to rest some time!", DateTimeOffset.Now, MessageFlags.ShowDialog);
                        await bandClient.NotificationManager.VibrateAsync(VibrationType.ThreeToneHigh);

                    }
                    else if ((currentRate <= minRate) && (!isMinNotified) && (isMaxNotified))
                    {
                        isMaxNotified = false;
                        isMinNotified = true;
                        await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "You can continue", DateTimeOffset.Now, MessageFlags.ShowDialog);
                        await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
                    }
                }
            }
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            SaveValuesForBackground();
            Deferral.Complete();
        }

    }
}

