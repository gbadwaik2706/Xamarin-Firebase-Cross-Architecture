using System;
namespace FCANoti.Dto
{
    public class LoginResponseDto : ResponseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}
