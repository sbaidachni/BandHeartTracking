using Microsoft.Band;
using Microsoft.Band.Sensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        public MainPage()
        {
            this.InitializeComponent();
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
                if (bandClient==null)
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
                }

            }
            catch (BandException ex)
            {
                VisualStateManager.GoToState(this, "NoBand", false);
            }
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (bandClient != null)
            {
                await bandClient.SensorManager.HeartRate.StartReadingsAsync();
                bandClient.Dispose();
            }
            base.OnNavigatedFrom(e);
        }

        private async void HeartRate_ReadingChanged(object sender, Microsoft.Band.Sensors.BandSensorReadingEventArgs<Microsoft.Band.Sensors.IBandHeartRateReading> e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                if (e.SensorReading.Quality == HeartRateQuality.Locked)
                    heartData.CurrentRate = e.SensorReading.HeartRate;
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
                }
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }
    }
}
