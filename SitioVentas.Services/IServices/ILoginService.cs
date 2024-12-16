using SitioVentas.Dto.Dto;
using SitioVentas.Entities.Entities;

namespace SitioVentas.Services.IServices
{
    public interface ILoginService
    {
        Task<List<UsuarioDto>> GetAll();

        Task<UsuarioDto> Get(int Id);

        Task<bool> Delete(int Id);

        Task<UsuarioDto> Insert(UsuarioDto usuario);

        Task<UsuarioDto> Update(int id, UsuarioDto usuario);

        Task<LoginResponseDto> ValidateUserAsync(string username, string password);
    }
}
