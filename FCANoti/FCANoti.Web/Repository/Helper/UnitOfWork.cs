using System;
using Microsoft.EntityFrameworkCore;

namespace FCANoti.Web.Repository.Helper
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly FCANotiDbContext Context;

        public UnitOfWork(FCANotiDbContext context)
        {
            Context = context;
        }

        public bool SaveChanges()
        {
            bool returnValue = true;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }

        #region Public Properties  

        public IEmployeeRepository _employeeRepoistory;
        public IEmployeeRepository EmployeeRepoistory
        {
            get => _employeeRepoistory ?? (_employeeRepoistory = new EmployeeRepository(Context));
            set => _employeeRepoistory = value;
        }
        #endregion


        #region IDisposable Support  
        private bool _disposedValue = false; // To detect redundant calls  

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                //dispose managed state (managed objects).  
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.  
            // set large fields to null.  

            _disposedValue = true;
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.  
        // ~UnitOfWork() {  
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
        //   Dispose(false);  
        // }  

        // This code added to correctly implement the disposable pattern.  
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.  
            // GC.SuppressFinalize(this);  
        }
        #endregion

    }
}
