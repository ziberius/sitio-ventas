using SitioVentas.Repository.Generics;
using SitioVentas.Entities.Entities;
using SitioVentas.Repository.IRepository;
using System.Data;

namespace SitioVentas.Repository.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnection db) : base(db)
        {


        }
    }
}
