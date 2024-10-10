using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoController : Controller
    {

        private readonly ILogger<GrupoController> _logger;
        private readonly IGrupoService _grupoService;


        public GrupoController(
            ILogger<GrupoController> logger,
            IGrupoService grupoService
            )
        {
            _logger = logger;
            _grupoService = grupoService;
        }

        [HttpGet]
        public List<GrupoDto> Get()
        {
            return _grupoService.GetAll().Result;
        }

    }
}