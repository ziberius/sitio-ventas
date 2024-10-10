using SitioVentas.Dto.Dto;

namespace SitioVentas.Services.IServices
{
    public interface ISubgrupoService
    {
        Task<List<SubgrupoDto>> GetAll();

        Task<SubgrupoDto> Get(int Id);

        Task<bool> Delete(int Id);

        Task<SubgrupoDto> Insert(SubgrupoDto subgrupo);

        Task<SubgrupoDto> Update(int id, SubgrupoDto subgrupo);
    }
}
