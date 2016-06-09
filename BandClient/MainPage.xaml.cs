using CommonData;
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
        HeartData heartData;

        private BackgroundTaskRegistration _deviceUseBackgroundTaskRegistration;
        private DeviceUseTrigger _deviceUseTrigger;
        BackgroundAccessStatus accessStatus;
        DeviceInformation device;

        public MainPage()
        {
            this.InitializeComponent();
            Application.Current.Suspending += Current_Suspending;
            Application.Current.Resuming += Current_Resuming; 
        }

        private async void Current_Resuming(object sender, object e)
        {
            await InitInterface();
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            if (bandClient != null)
            {
                var deferral=e.SuspendingOperation.GetDeferral();
                if (bandClient != null)
                {
                    bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
                    bandClient.Dispose();
                }
                deferral.Complete();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitInterface();
            base.OnNavigatedTo(e); 
        }

        private async Task InitInterface()
        {
            heartData = new HeartData();
            this.DataContext = heartData;
            device = (await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.FromUuid(new Guid("A502CA9A-2BA5-413C-A4E0-13804E47B38F"))))).FirstOrDefault();

            VisualStateManager.GoToState(this, "Loading", false);

            if ((heartData.IsStarted) && (IsBackgroundRunning()))
            {
                VisualStateManager.GoToState(this, "NormalStarted", false);
                _deviceUseBackgroundTaskRegistration.Progress += _deviceUseBackgroundTaskRegistration_Progress;
                return;
            }

            KillBackground();

            heartData.IsStarted = false;
            heartData.IsMaxNotified = false;
            heartData.IsMinNotified = false;

            await InitBand();
        }

        private async Task InitBand()
        {
            try
            {
                IBandInfo[] pairedBands;

                pairedBands = await BandClientManager.Instance.GetBandsAsync();
                
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
                    bandClient.SensorManager.HeartRate.ReadingChanged += HeartRate_ReadingChanged;
                    bool res = await bandClient.SensorManager.HeartRate.StartReadingsAsync();
                    
                    VisualStateManager.GoToState(this, "Normal", false);
                    
                    BandTile myTile = new BandTile(heartData.MyTileId)
                    {
                        Name = "My Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };

                    IEnumerable<BandTile> tiles = await bandClient.TileManager.GetTilesAsync();

                    if ((await bandClient.TileManager.GetRemainingTileCapacityAsync() > 0) && (tiles.Count() == 0))
                    {
                        await bandClient.TileManager.AddTileAsync(myTile);
                    }
                }
            }
            catch (Exception ex)
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

        private async void HeartRate_ReadingChanged(object sender, Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandHeartRateReading> e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                if (e.SensorReading.Quality == HeartRateQuality.Locked)
                {
                    heartData.CurrentRate = e.SensorReading.HeartRate;
                }
             });
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await InitInterface();
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
                    heartData.IsStarted = true;
                    heartData.IsMinNotified = false;
                    heartData.IsMaxNotified = false;
                    await bandClient.NotificationManager.SendMessageAsync(heartData.MyTileId, "Workload Demo", "The workload is started", DateTimeOffset.Now, MessageFlags.ShowDialog);
                    ReleaseBand();
                    await ActivateBackground();
                }
            }
        }

        private void ReleaseBand()
        {
            bandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
            bandClient.Dispose();
            bandClient = null;
        }

        private async void stopButton_Click(object sender, RoutedEventArgs e)
        {
            KillBackground();
            heartData.IsStarted = false;
            await InitBand();
            //await bandClient.NotificationManager.SendMessageAsync(heartData.MyTileId, "Workload Demo", "Your workloaded is stopped", DateTimeOffset.Now, MessageFlags.ShowDialog);

        }

        private void KillBackground()
        {
            var t = (from a in BackgroundTaskRegistration.AllTasks.Values
                    where a.Name.Equals("BandTaskInBackground")
                    select a).FirstOrDefault();
            if (t != null)
                t.Unregister(true);
        }

        private bool IsBackgroundRunning()
        {
            var res = (from a in BackgroundTaskRegistration.AllTasks.Values
                      where a.Name.Equals("BandTaskInBackground")
                      select a).FirstOrDefault();
            if (res != null)
            {
                _deviceUseBackgroundTaskRegistration = res as BackgroundTaskRegistration;
                return true;
            }
            return false;
        }

        private async Task ActivateBackground()
        {
            _deviceUseTrigger = new DeviceUseTrigger();
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
                _deviceUseBackgroundTaskRegistration.Progress += _deviceUseBackgroundTaskRegistration_Progress;
                var triggerResult = await _deviceUseTrigger.RequestAsync(device.Id);
            }
        }

        private async void _deviceUseBackgroundTaskRegistration_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                heartData.CurrentRate = (int)args.Progress;
            });
            
        }
    }
}
