using System;
using System.Text;
using System.Threading.Tasks;
using FCANoti.DependacyServices;
using FCANoti.Dto;
using FCANoti.Helper;
using FCANoti.WebService;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace FCANoti.Services
{
    public class LoginService
    {

        public async Task<LoginResponseDto> CheckLogin(string username, string encryptedPassword)
        {
            LoginRequestDto dto = new LoginRequestDto
            {
                MobileNo = username,
                Password = encryptedPassword
            };

            //IRestClient<LoginRequestDto, LoginResponseDto> rest = new RestClient<LoginRequestDto, LoginResponseDto>();
            //LoginResponseDto res = await rest.Post(dto, "Login/CheckLogin", MediaType.Json, Encoding.UTF8);
            // Temp Login
            LoginResponseDto res = new LoginResponseDto()
            {
                Message = "success",
                Name = "Emp1",
                Status = true,
                UserId = 100
            };

            if (res.Status)
            {

                // Save in Shared Preferance.
                SharedPreferences.UserId = res.UserId;

                UpdateNotificationToken();
            }

            return res;
        }

        public async void UpdateNotificationToken()
        {
            try
            {
                string token = SharedPreferences.FirebaseToken;
                if (string.IsNullOrEmpty(token))
                {
                    token = await CrossFirebasePushNotification.Current.GetTokenAsync();
                    SharedPreferences.FirebaseToken = token;
                }

                // Step 1
                // Add or update in DB, Call API
                // Parameters - UserID, UserType, Token, DeviceID
                // Check if current device is registered with other user(s), if yes, then set Token to Null


                IDevice deviceInfo = DependencyService.Get<IDevice>();
                string imei = deviceInfo.GetIdentifier();

                UserTokenDto dto = new UserTokenDto()
                {
                    DeviceID = imei,
                    UserID = SharedPreferences.UserId,
                    Token = token
                };

                IRestClient<UserTokenDto, ResponseDto> rest = new RestClient<UserTokenDto, ResponseDto>();
                ResponseDto res = await rest.Post(dto, "UserCommon/AddUpdateToken", MediaType.Json, Encoding.UTF8);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void UpdateTokenToNull(long uniqueId)
        {
            try
            {
                // Update token to empty in db
                IDevice deviceInfo = DependencyService.Get<IDevice>();
                string imei = deviceInfo.GetIdentifier();

                DeviceCheckTokenDto dtoDevice = new DeviceCheckTokenDto()
                {
                    DeviceID = imei,
                    UserID = uniqueId,
                };

                IRestClient<DeviceCheckTokenDto, ResponseDto> rest1 = new RestClient<DeviceCheckTokenDto, ResponseDto>();
                ResponseDto res1 = await rest1.Post(dtoDevice, "UserCommon/DeviceLogout", MediaType.Json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
