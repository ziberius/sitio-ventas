using SitioVentas.Repository.Generics;
using SitioVentas.Entities.Entities;
using SitioVentas.Repository.IRepository;
using System.Data;

namespace SitioVentas.Repository.Repository
{
    public class TipoRepository : GenericRepository<Tipo>, ITipoRepository
    {
        public TipoRepository(IDbConnection db) : base(db)
        {
        }
    }
}
