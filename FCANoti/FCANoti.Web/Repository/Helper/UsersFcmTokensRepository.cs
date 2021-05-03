using System;
using FCANoti.Web.Entity;

namespace FCANoti.Web.Repository.Helper
{
    public interface IUsersFcmTokensRepository : IRepository<UsersFcmTokens>
    {
    }

    public class UsersFcmTokensRepository : Repository<UsersFcmTokens>, IUsersFcmTokensRepository
    {
        private FCANotiDbContext _db;

        public UsersFcmTokensRepository(FCANotiDbContext context) : base(context)
        {
            _db = context;
        }
    }
}
