using System;
namespace FCANoti.Dto
{
    public class NotificationRequestDto
    {
        public long UserID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int BadgeCount { get; set; }
    }
}
