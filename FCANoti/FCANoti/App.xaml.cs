using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FCANoti.Services;
using FCANoti.Views;
using Plugin.FirebasePushNotification;
using FCANoti.Helper;

namespace FCANoti
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            InitNotifications();

            MainPage = new LoginPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region Notifications
        void InitNotifications()
        {
            // Token event usage sample:
            CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
            {
                SharedPreferences.FirebaseToken = p.Token;

                // Update to DB
                LoginService loginService = new LoginService();
                if (SharedPreferences.UserId != 0)
                    loginService.UpdateNotificationToken();
            };

            // Push message received event usage sample:
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };

            // Push message opened event usage sample:
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };

        }
        #endregion
    }
}
