using SitioVentas.Repository.Generics;
using SitioVentas.Entities.Entities;
using SitioVentas.Repository.IRepository;
using System.Data;

namespace SitioVentas.Repository.Repository
{
    public class GrupoRepository : GenericRepository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(IDbConnection db) : base(db)
        {
        }
    }
}
