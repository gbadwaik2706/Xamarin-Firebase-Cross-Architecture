using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using Plugin.FirebasePushNotification;
using Android.Graphics;
using FCANoti.Helper;

namespace FCANoti.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            //FirebaseApp.InitializeApp(this);

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = MainActivity.CHANNEL_ID;

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            FirebasePushNotificationManager.NotificationActivityType = typeof(MainActivity);

            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true, true, true);
            SharedPreferences.FirebaseToken = string.Empty;
#else
            FirebasePushNotificationManager.Initialize(this, false, true, true);
#endif

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    var messageTitle = p.Data["title"].ToString();
                    var messageBody = p.Data["body"].ToString();

                    SendLocalNotification(messageTitle, messageBody);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            };
        }

        private void SendLocalNotification(string title, string body)
        {
            try
            {
                //_notificationBadgeCount++;
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra("message", body);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                //int notificationIconId =
                //Resources.GetIdentifier(Resources.GetDrawable(), "drawable",
                //AppInfo.PackageName);
                //MainActivity.LogInfo($"Found notification icon with id: {notificationIconId}");

                var notificationBuilder = new NotificationCompat.Builder(this,
                                            MainActivity.CHANNEL_ID)
                                            .SetContentTitle(title)
                                            .SetSmallIcon(Resource.Drawable.xamarin_logo) //.SetSmallIcon(notificationIconId) //.SetSmallIcon(ApplicationInfo.Icon)
                                            .SetColor(Color.SkyBlue)
                                            .SetContentText(body)
                                            .SetAutoCancel(true)
                                            .SetShowWhen(false)
                                            .SetContentIntent(pendingIntent)
                                            .SetPriority((int)Android.App.NotificationPriority.Max);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    notificationBuilder.SetChannelId(MainActivity.CHANNEL_ID);
                }

                var notificationManager = NotificationManager.FromContext(this);
                notificationManager.Notify(0, notificationBuilder.Build());
                //CrossBadge.Current.SetBadge(_notificationBadgeCount);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
