using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BandClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IBandClient bandClient;
        bool isStarted = false;
        Guid myTileId;
        bool isMaxNotified=false;
        bool isMinNotified = false;

        private BackgroundTaskRegistration _deviceUseBackgroundTaskRegistration;
        private DeviceUseTrigger _deviceUseTrigger;
        BackgroundAccessStatus accessStatus;
        DeviceInformation device;

        public MainPage()
        {
            this.InitializeComponent();
            _deviceUseTrigger = new DeviceUseTrigger();
            Application.Current.Suspending += Current_Suspending;
            Application.Current.Resuming += Current_Resuming; 
        }

        private async void Current_Resuming(object sender, object e)
        {
            await InitBand();
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            if (bandClient != null)
            {
                var deferral=e.SuspendingOperation.GetDeferral();
                bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
                bandClient.Dispose();
                if (isStarted)
                {
                    SaveValuesForBackground();
                }
                deferral.Complete();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitBand();
            base.OnNavigatedTo(e);
        } 

        private async Task InitBand()
        {
            VisualStateManager.GoToState(this, "Loading", false);

            IBandInfo[] pairedBands;

            pairedBands = await BandClientManager.Instance.GetBandsAsync();

            try
            {
                bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
                if (bandClient.SensorManager.HeartRate.GetCurrentUserConsent() != UserConsent.Granted)
                {
                    var result = await bandClient.SensorManager.HeartRate.RequestUserConsentAsync();
                }
                if (bandClient.SensorManager.HeartRate.GetCurrentUserConsent() != UserConsent.Granted)
                {
                    VisualStateManager.GoToState(this, "NoPermissions", false);
                }
                else
                {
                    device = (await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(new Guid("A502CA9A-2BA5-413C-A4E0-13804E47B38F"))))).FirstOrDefault();
                    bandClient.SensorManager.HeartRate.ReadingChanged += HeartRate_ReadingChanged;
                    bool res = await bandClient.SensorManager.HeartRate.StartReadingsAsync();

                    LoadValuesFromBackground();
                    if (isStarted)
                    {
                        VisualStateManager.GoToState(this, "NormalStarted", false);
                    }
                    else
                    {
                        VisualStateManager.GoToState(this, "Normal", false);
                    }
                    

                    myTileId = new Guid("5E67A3C2-39D1-4F9B-BBFF-0D81CCE6D317");
                    BandTile myTile = new BandTile(myTileId)
                    {
                        Name = "My Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };

                    IEnumerable<BandTile> tiles = await bandClient.TileManager.GetTilesAsync();

                    if ((await bandClient.TileManager.GetRemainingTileCapacityAsync() > 0)&&(tiles.Count()==0))
                    {
                        await bandClient.TileManager.AddTileAsync(myTile);
                    }
                }

            }
            catch (BandException ex)
            {
                VisualStateManager.GoToState(this, "NoBand", false);
            }
        }

        private async Task<BandIcon> LoadIcon(string uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(fileStream);
                return bitmap.ToBandIcon();
            }
        }

        private void SaveValuesForBackground()
        {
            ApplicationData.Current.LocalSettings.Values["isStarted"] = isStarted;
            ApplicationData.Current.LocalSettings.Values["myTileId"] = myTileId;
            ApplicationData.Current.LocalSettings.Values["isMaxNotified"] = isMaxNotified;
            ApplicationData.Current.LocalSettings.Values["isMinNotified"] = isMinNotified;
            ApplicationData.Current.LocalSettings.Values["maxRate"] = heartData.MaxRate;
            ApplicationData.Current.LocalSettings.Values["minRate"] = heartData.MinRate;
        }

        private void LoadValuesFromBackground()
        {
            if (ApplicationData.Current.LocalSettings.Values["isStarted"]!=null)
            {
                isStarted = (bool)ApplicationData.Current.LocalSettings.Values["isStarted"];
                isMinNotified = (bool)ApplicationData.Current.LocalSettings.Values["isMinNotified"];
                isMaxNotified = (bool)ApplicationData.Current.LocalSettings.Values["isMaxNotified"];
                heartData.MaxRate = (int)ApplicationData.Current.LocalSettings.Values["maxRate"];
                heartData.MinRate = (int)ApplicationData.Current.LocalSettings.Values["minRate"];
            }
        }

        private async void HeartRate_ReadingChanged(object sender, Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandHeartRateReading> e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            async () =>
            {
                if (e.SensorReading.Quality == HeartRateQuality.Locked)
                {
                    heartData.CurrentRate = e.SensorReading.HeartRate;
                    if (isStarted)
                    {
                        if ((heartData.CurrentRate>=heartData.MaxRate)&&(!isMaxNotified))
                        {
                            isMaxNotified = true;
                            isMinNotified = false;
                            await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "You have to rest some time!", DateTimeOffset.Now, MessageFlags.ShowDialog);
                            await bandClient.NotificationManager.VibrateAsync(VibrationType.ThreeToneHigh);

                        }
                        else if ((heartData.CurrentRate <= heartData.MinRate)&&(!isMinNotified)&&(isMaxNotified))
                        {
                            isMaxNotified = false;
                            isMinNotified = true;
                            await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "You can continue", DateTimeOffset.Now, MessageFlags.ShowDialog);
                            await bandClient.NotificationManager.VibrateAsync(VibrationType.TwoToneHigh);
                        }
                    }
                }
             });
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await InitBand();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            int idx = heartData.Rates.IndexOf(heartData.CurrentRate);
            if (idx < 0) idx = 0;
            heartData.CurrentRateIndex = idx;
            ContentDialogResult result = await termsOfUseContentDialog.ShowAsync();
            if (result==ContentDialogResult.Primary)
            {
                int idx1 = minBox.SelectedIndex;
                int idx2 = maxBox.SelectedIndex;
                if (idx1<idx2)
                {
                    VisualStateManager.GoToState(this, "NormalStarted", false);
                    heartData.MinRate = heartData.Rates[idx1];
                    heartData.MaxRate = heartData.Rates[idx2];
                    isStarted = true;
                    isMinNotified = false;
                    isMaxNotified = false;
                    await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "The workload is started", DateTimeOffset.Now, MessageFlags.ShowDialog);
                }
                
            }
        }

        private async void stopButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
            await bandClient.NotificationManager.SendMessageAsync(myTileId, "Workload Demo", "Your workloaded is stopped", DateTimeOffset.Now, MessageFlags.ShowDialog);
            isStarted = false;
        }

        private void KillBackground()
        {
            foreach (var backgroundTask in BackgroundTaskRegistration.AllTasks.Values)
            {
                ((BackgroundTaskRegistration)backgroundTask).Unregister(true);
            }
        }

        private async void ActivateBackground()
        {
            accessStatus = await BackgroundExecutionManager.RequestAccessAsync();

            if ((BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity == accessStatus) ||
                (BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity == accessStatus))
            {
                var backgroundTaskBuilder = new BackgroundTaskBuilder()
                {
                    Name = "BandTaskInBackground", 
                    TaskEntryPoint = "BandBackgroundTask.BandTaskInBackground"
                };
                backgroundTaskBuilder.SetTrigger(_deviceUseTrigger);
                _deviceUseBackgroundTaskRegistration = backgroundTaskBuilder.Register();

                var triggerResult = await _deviceUseTrigger.RequestAsync(device.Id);
            }
        }
    }
}
