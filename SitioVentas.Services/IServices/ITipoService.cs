using SitioVentas.Dto.Dto;

namespace SitioVentas.Services.IServices
{
    public interface ITipoService
    {
        Task<List<TipoDto>> GetAll();

        Task<TipoDto> Get(int Id);

        Task<bool> Delete(int Id);

        Task<TipoDto> Insert(TipoDto tipo);

        Task<TipoDto> Update(int id, TipoDto tipo);
    }
}
