using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;


        public ItemController(
            ILogger<ItemController> logger,
            IItemService itemService
            )
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpGet]
        public List<ItemDto> Get()
        {
            return _itemService.GetAll().Result;
        }

        [HttpGet("items/{pageNumber}/{pageSize}")]

        public async Task<PaginatedData<ItemDto>> GetPaginatedData(int pageNumber, int pageSize)
        {
            // Obtén los datos totales
            var totalItems = await _itemService.GetTotalNoticias();

            // Realiza la paginación
            var items = await _itemService.GetPaginated(pageNumber, pageSize);

            // Crea el objeto paginado
            var paginatedData = new PaginatedData<ItemDto>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return paginatedData;
        }

        [HttpPost("item")]
        //[Authorize(Roles = RoleConstants.MANTWEB_NOSOTROS_ADMIN)]
        public async Task<ItemDto> NewsInfoItem([FromBody] ItemDto newsInfoItemDto)
        {
            try
            {
                return await _itemService.Insert(newsInfoItemDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}