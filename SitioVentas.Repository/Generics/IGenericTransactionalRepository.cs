using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Repository.Generics
{
    public interface IGenericTransactionalRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
 
    }
}
