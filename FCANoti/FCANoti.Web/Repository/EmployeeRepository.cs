using System;
using FCANoti.Web.Entity;
using FCANoti.Web.Repository.Helper;

namespace FCANoti.Web.Repository
{

    public interface IEmployeeRepository : IRepository<Employees>
    {
    }

    public class EmployeeRepository : Repository<Employees>, IEmployeeRepository
    {
        private FCANotiDbContext _db;

        public EmployeeRepository(FCANotiDbContext context) : base(context)
        {
            _db = context;
        }
    }
}
