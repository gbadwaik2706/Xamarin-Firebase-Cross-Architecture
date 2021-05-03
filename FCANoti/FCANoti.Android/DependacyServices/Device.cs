using System;
using Android.Provider;
using FCANoti.DependacyServices;
using FCANoti.Droid.DependacyServices;

[assembly: Xamarin.Forms.Dependency(typeof(Device))]
namespace  FCANoti.Droid.DependacyServices
{
    public class Device : IDevice
    {
        public string GetIdentifier()
        {
            return Settings.Secure.GetString(Xamarin.Forms.Forms.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}