using SitioVentas.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Services.IServices
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetAll();

        Task<ItemDto> Get(int Id);

        Task<bool> Delete(int Id);

        Task<ItemDto> Insert(ItemDto item);

        Task<ItemDto> Update(int id, ItemDto item);

        Task<List<FotoDto>> GetFotos(int Id);

        Task<List<ItemDto>> GetPaginated(int pageNumber, int pageSize);

        Task<List<ItemDto>> GetPaginatedFiltered(PaginatedFilteredRequestDto request);

        Task<int> GetTotalNoticias();
    }
}
