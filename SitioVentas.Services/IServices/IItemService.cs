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
    }
}
