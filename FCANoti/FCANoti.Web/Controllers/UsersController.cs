using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCANoti.Dto;
using FCANoti.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCANoti.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        IUserCommonService _userCommon;

        public UsersController(IUserCommonService customerAddressService)
        {
            _userCommon = customerAddressService;
        }


        /// <summary>
        /// Check if current device is registered with other user(s), if yes, then set Token to Null for other devices
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUpdateToken")]
        [ActionName("AddUpdateToken")]
        public IActionResult AddUpdateToken(UserTokenDto dto)
        {
            try
            {
                ResponseDto res = _userCommon.AddEditToken(dto);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }


        /// <summary>
        /// Set token to null on device logout
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeviceLogout")]
        [ActionName("DeviceLogout")]
        public IActionResult DeviceLogout(DeviceDto dto)
        {
            try
            {
                ResponseDto res = _userCommon.DeviceLogout(dto);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

    }
}
