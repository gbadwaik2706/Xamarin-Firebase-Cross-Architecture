using System;
namespace FCANoti.Web.Repository.Helper
{
    public interface IUnitOfWork : IDisposable
    {
        bool SaveChanges();

        //IEmployeeRepository EmployeeRepoistory { get; set; }
    }
}
