using SitioVentas.Constants.Constants;
using SitioVentas.Repository.Repository;
using System;
namespace SitioVentas.Services.Base
{
    public abstract class BaseService
    {
        public void SetearCamposInicio<TEntity>(TEntity entity) where TEntity: SitioVentas.Entities.Entities.Base.Base
        {
            string userCreate = entity.UsuarioCreador;
            try
            {
                entity.UsuarioCreador = string.IsNullOrEmpty(userCreate) ? "Usuario sin Asignar" : userCreate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetearCamposAuditoria<TEntity>(TEntity entity) where TEntity : SitioVentas.Entities.Entities.Base.Base
        {
            string userLastUpdate = entity.UsuarioActualizador;

            entity.UsuarioActualizador = string.IsNullOrEmpty(userLastUpdate) ? "Usuario sin Asignar" : userLastUpdate;
            entity.Activo = true;
            entity.FechaActualizacion = DateTime.Now;
            if (entity.FechaCreacion == null)
            {
                entity.FechaCreacion = DateTime.Now;
            }
        }
    }
}
