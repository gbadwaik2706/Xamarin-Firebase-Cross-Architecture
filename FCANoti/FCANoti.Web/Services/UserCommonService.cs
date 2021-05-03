using System;
using System;
using System.Linq;
using FCANoti.Dto;
using FCANoti.Web.Entity;
using FCANoti.Web.Repository.Helper;
using LinqKit;

namespace FCANoti.Web.Services
{
    public interface IUserCommonService
    {
        ResponseDto AddEditToken(UserTokenDto dto);
        ResponseDto DeviceLogout(DeviceDto dto);
    }

    public class UserCommonService : IUserCommonService
    {
        IUsersFcmTokensRepository _usersFcmRepo;

        public UserCommonService(IUsersFcmTokensRepository repository)
        {
            _usersFcmRepo = repository;
        }

        public ResponseDto AddEditToken(UserTokenDto dto)
        {
            ResponseDto response = new ResponseDto();

            // Setting existing Tokens to Null for all kind of users having same device Ids.
            var predicateDelete = PredicateBuilder.New<UsersFcmTokens>();
            predicateDelete.And(s => s.DeviceID == dto.DeviceID);
            var usedTokens = _usersFcmRepo.Get(predicateDelete).AsEnumerable().ToList();
            foreach (var item in usedTokens)
            {
                item.Token = null;
                _usersFcmRepo.Update(item);
                var resUpdate = _usersFcmRepo.SaveChanges();
            }

            var predicate = PredicateBuilder.New<UsersFcmTokens>();
            predicate.And(s => s.DeviceID == dto.DeviceID &&
                               s.UserID == dto.UserID);

            UsersFcmTokens userToken = _usersFcmRepo.Get(predicate).AsEnumerable().ToList().FirstOrDefault();
            if (userToken == null)
            {
                // Add New
                UsersFcmTokens token = new UsersFcmTokens
                {
                    Token = dto.Token,
                    UserID = dto.UserID,
                    DeviceID = dto.DeviceID
                };
                _usersFcmRepo.Add(token);
                var isCreated = _usersFcmRepo.SaveChanges();
                if (isCreated)
                {
                    response.Status = true;
                    response.Message = "created";
                }
                else
                {
                    response.Status = false;
                    response.Message = "not_created";
                }
            }
            else
            {
                // Update the same
                userToken.Token = dto.Token;
                _usersFcmRepo.Update(userToken);
                var resUpdate = _usersFcmRepo.SaveChanges();

                if (resUpdate)
                {
                    response.Status = true;
                    response.Message = "updated";
                }
                else
                {
                    response.Status = false;
                    response.Message = "not_updated";
                }
            }

            return response;
        }


        // Set Device to Null
        public ResponseDto DeviceLogout(DeviceDto dto)
        {
            ResponseDto response = new ResponseDto();

            var predicate = PredicateBuilder.New<UsersFcmTokens>();
            predicate.And(s => s.DeviceID == dto.DeviceID);

            var userTokens = _usersFcmRepo.Get(predicate).AsEnumerable().ToList();
            if (userTokens != null)
            {
                foreach (var userToken in userTokens)
                {
                    userToken.Token = null;
                    _usersFcmRepo.Update(userToken);
                    var resUpdate = _usersFcmRepo.SaveChanges();

                    if (resUpdate)
                    {
                        response.Status = true;
                        response.Message = "updated";
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "not_updated";
                    }
                }
            }
            else
            {
                response.Status = false;
                response.Message = "not_updated";
            }

            return response;
        }
    }
}
