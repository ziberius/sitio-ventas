using SitioVentas.Dto.Dto;

namespace SitioVentas.Services.IServices
{
    public interface IGrupoService
    {
        Task<List<GrupoDto>> GetAll();

        Task<GrupoDto> Get(int Id);

        Task<bool> Delete(int Id);

        Task<GrupoDto> Insert(GrupoDto grupo);

        Task<GrupoDto> Update(int id, GrupoDto grupo);
    }
}
