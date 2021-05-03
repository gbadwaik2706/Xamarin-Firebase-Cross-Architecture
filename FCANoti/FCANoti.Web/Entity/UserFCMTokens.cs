using System;
using System.ComponentModel.DataAnnotations;

namespace FCANoti.Web.Entity
{
    public class UsersFcmTokens
    {
        [Key]
        public long ID { get; set; }

        public long? UserID { get; set; }

        public string DeviceID { get; set; }

        public string Token { get; set; }
    }
}
