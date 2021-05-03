using System;
namespace FCANoti.Dto
{
    public class UserTokenDto
    {
        public long UserID { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public string DeviceID { get; set; }
    }

    public class DeviceCheckTokenDto
    {
        public long UserID { get; set; }
        public string UserType { get; set; }
        public string DeviceID { get; set; }
    }

    public class DeviceDto
    {
        public string DeviceID { get; set; }
    }
}
