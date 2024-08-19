using SitioVentas.Repository.Generics;
using SitioVentas.Entities.Entities;
using SitioVentas.Repository.IRepository;
using System.Data;

namespace SitioVentas.Repository.Repository
{
    public class SubGrupoRepository : GenericRepository<SubGrupo>, ISubGrupoRepository
    {
        public SubGrupoRepository(IDbConnection db) : base(db)
        {
        }
    }
}
