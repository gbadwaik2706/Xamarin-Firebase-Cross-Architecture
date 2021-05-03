using System;
namespace FCANoti.WebService
{
    public static class Locator
    {
        public static Uri BaseApiUrl { get; } = new Uri($"{App.AzureBackendUrl}/");
    }
}
