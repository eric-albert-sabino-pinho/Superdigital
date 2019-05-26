using System;

namespace Superdigital.Entity.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void SaveChanges();
    }
}
