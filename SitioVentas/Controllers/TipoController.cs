using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoController : Controller
    {

        private readonly ILogger<TipoController> _logger;
        private readonly ITipoService _tipoService;


        public TipoController(
            ILogger<TipoController> logger,
            ITipoService tipoService
            )
        {
            _logger = logger;
            _tipoService = tipoService;
        }

        [HttpGet]
        public List<TipoDto> Get()
        {
            return _tipoService.GetAll().Result;
        }

    }
}