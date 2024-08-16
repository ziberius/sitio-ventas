using System;

namespace SitioVentas.Repository.Generics
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void SetRepository(Type tipo);
        T GetRepository<T>();
    }
}
