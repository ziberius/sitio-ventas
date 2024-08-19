using SitioVentas.Repository.Generics;
using SitioVentas.Entities.Entities;
using SitioVentas.Repository.IRepository;
using System.Data;

namespace SitioVentas.Repository.Repository
{
    public class FotoRepository : GenericRepository<Foto>, IFotoRepository
    {
        public FotoRepository(IDbConnection db) : base(db)
        {
        }
    }
}
