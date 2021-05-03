using System;
using Xamarin.Essentials;

namespace FCANoti.Helper
{
    public class SharedPreferences
    {
        public static long UserId
        {
            get => Preferences.Get("UserId", (long)0);
            set => Preferences.Set("UserId", value);
        }

        public static string FirebaseToken
        {
            get => Preferences.Get("FirebaseToken", string.Empty);
            set => Preferences.Set("FirebaseToken", value);
        }
    }
}
