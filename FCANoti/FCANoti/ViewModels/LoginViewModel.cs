using Acr.UserDialogs;
using FCANoti.Services;
using FCANoti.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace FCANoti.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }

        #endregion

        #region Commands
        public ICommand SignInCommand { protected set; get; }
        public ICommand ForgetPasswordCommand { protected set; get; }
        #endregion

        public LoginViewModel()
        {
            SignInCommand = new Command(OnSignInAsync);
            ForgetPasswordCommand = new Command(OnForgetPassword);
        }


        #region Methods

        private async void OnSignInAsync(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    await PopupNavigation.Instance.PushAsync(new StatusPopupPage(
                            "Please enter your Username & Password and please try again",
                            "Invalid Login", showAppVersion: false));
                    return;
                }

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await PopupNavigation.Instance.PushAsync(new StatusPopupPage(
                        "Please connect to internet.",
                        "No internet."));
                    return;
                }

                IsBusy = true;
                UserDialogs.Instance.ShowLoading("Logging In...");

                //string encryptedPassword = Md5Converter.ToMd5(Password);
                string encryptedPassword = Password;

                LoginService service = new LoginService();
                var res = await service.CheckLogin(Username, encryptedPassword);

                IsBusy = false;

                if (res.Status)
                {
                    //PopupNavigation.Instance.PushAsync(new LoginSuccessPopupPage());
                    MaterialDialog.Instance.SnackbarAsync(message: "Login Successful.",
                                       actionButtonText: "Dismiss",
                                       msDuration: 2000);
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new StatusPopupPage("Invalid Login.", "Alert"));
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await PopupNavigation.Instance.PushAsync(new StatusPopupPage(
                            "Something went wrong, please try again. ER CODE: 00",
                            "Invalid Login",
                            showAppVersion: false));
                return;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private void OnForgetPassword(object obj)
        {
        }
        #endregion

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
