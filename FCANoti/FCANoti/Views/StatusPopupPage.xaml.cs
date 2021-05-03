using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FCANoti.Views
{
    public partial class StatusPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        string _message;
        string _title;
        string _icon;
        string _route;
        string _routeName;
        bool _autoClose;
        int _autoCloseTime;
        bool _showAppVersion = false;

        public StatusPopupPage(string message, string title, string icon = null,
                                       string route = null, string routeName = null,
                                       bool autoClose = false, int autoCloseTime = 5000,
                                       bool showAppVersion = false)
        {
            InitializeComponent();
            this.BackgroundColor = (Color.Black).MultiplyAlpha(0.50);

            _message = message;
            _title = title;
            _icon = icon;
            _route = route;
            _routeName = routeName;
            _autoClose = autoClose;
            _autoCloseTime = autoCloseTime;
            _showAppVersion = showAppVersion;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_icon != null)
                img.Source = ImageSource.FromResource(_icon, typeof(App).GetTypeInfo().Assembly);
            else
                img.IsVisible = false;

            lblTitle.Text = _title;
            lblMessage.Text = _message;

            if (_route != null)
            {
                btnRedirect.IsVisible = true;
                btnRedirect.Text = _routeName;
            }

            if (_autoClose)
                HidePopup();

            lblAppVersion.IsVisible = false;
            if (_showAppVersion)
            {
                lblAppVersion.IsVisible = true;
                lblAppVersion.Text = $"App Version {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})"; ;
            }
        }

        private async void HidePopup()
        {
            await Task.Delay(_autoCloseTime);

            if (PopupNavigation.Instance.PopupStack.Contains(this))
                await PopupNavigation.Instance.RemovePageAsync(this);
        }

        async void btnClose_Clicked(System.Object sender, System.EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Contains(this))
                await PopupNavigation.Instance.RemovePageAsync(this);
        }

        async void btnRedirect_Clicked(System.Object sender, System.EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Contains(this))
                await PopupNavigation.Instance.RemovePageAsync(this);
            await Shell.Current.GoToAsync("//tabbar/tab/More/PaymentRequestPage", true);
        }



        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
