using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
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
    }
}