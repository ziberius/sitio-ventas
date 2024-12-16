using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [Authorize]
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
        public async Task<List<ItemDto>> Get()
        {
            return await _itemService.GetAll();
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

        [HttpPost]
        //[Authorize(Roles = RoleConstants.MANTWEB_NOSOTROS_ADMIN)]
        public async Task<ItemDto> SaveProduct([FromBody] ItemDto producto)
        {
            try
            {
                return await _itemService.Insert(producto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> UpdateProduct(int id, [FromBody] ItemDto producto)
        {
            try
            {
                var update = await _itemService.Update(id, producto);
                if (update == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(update);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("fotos/{itemId}")]
        public async Task<List<FotoDto>> GetFotos(int itemId)
        {
            return await _itemService.GetFotos(itemId);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                return await _itemService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }

        [HttpGet("items/destacados")]
        public async Task<List<ItemDto>> GetDestacados()
        {
            return await _itemService.GetDestacados();
        }

        [HttpGet("items/subgrupo/{id}")]
        public async Task<List<ItemDto>> GetBySubgrupo(int subgrupo)
        {
            return await _itemService.GetBySugrupo(subgrupo);
        }

        [HttpGet("items/grupo/{id}")]
        public async Task<List<ItemDto>> GetByGrupo(int grupo)
        {
            return await _itemService.GetByGrupo(grupo);
        }

    }
}