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
using CommonData;
using Windows.UI.Xaml;

namespace BandBackgroundTask
{
    public sealed class BandTaskInBackground : IBackgroundTask
    {
        private BackgroundTaskDeferral Deferral;
        IBandClient bandClient;
        HeartData heartData;
        IBackgroundTaskInstance taskInstance;
        private bool IsMaxNotified=false;
        private bool IsMinNotified = false;
        private int MinRate = 0;
        private int MaxRate = 0;
        bool flag = true;
        bool isSent = true;


        public void Run(IBackgroundTaskInstance taskInstance)
        {
            this.taskInstance = taskInstance;
            Deferral = taskInstance.GetDeferral();
            heartData = new HeartData();
            MinRate = heartData.MinRate;
            MaxRate = heartData.MaxRate;
            taskInstance.Canceled += OnCanceled;
            while (flag)
            {
                if (isSent)
                {
                    if (bandClient != null)
                    {
                        bandClient.Dispose();
                        bandClient = null;
                    }
                    isSent = false;
                    InitBand().Wait();
                }
            }
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
            }
            catch (BandException ex)
            {
                Deferral.Complete();
            }
        }

        private async void Timer_Tick(object sender, object e)
        {
            await bandClient.NotificationManager.SendMessageAsync(heartData.MyTileId, "Workload Demo", "The workload is started", DateTimeOffset.Now, MessageFlags.ShowDialog);
        }

        private async void HeartRate_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
        {
            
            
            if (e.SensorReading.Quality == HeartRateQuality.Locked)
            {
                int currentRate = e.SensorReading.HeartRate;
                taskInstance.Progress = (uint)currentRate;

                if ((currentRate >= MaxRate) && (!IsMaxNotified))
                {
                    IsMaxNotified = true;
                    IsMinNotified = false;
                    await bandClient.NotificationManager.SendMessageAsync(heartData.MyTileId, "Workload Demo", "You have to rest some time!", DateTimeOffset.Now, MessageFlags.ShowDialog);
                    await bandClient.NotificationManager.VibrateAsync(VibrationType.ThreeToneHigh);

                }
                else if ((currentRate <= MinRate) && (!IsMinNotified) && (IsMaxNotified))
                {
                    IsMaxNotified = false;
                    IsMinNotified = true;
                    await bandClient.NotificationManager.SendMessageAsync(heartData.MyTileId, "Workload Demo", "You can continue", DateTimeOffset.Now, MessageFlags.ShowDialog);
                    await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
                }
            }
            bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
            isSent = true;
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            flag = false;
            if (bandClient!=null)
            {
                bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
                bandClient.Dispose();
                bandClient = null;
            }
            Deferral.Complete();
        }

    }
}

