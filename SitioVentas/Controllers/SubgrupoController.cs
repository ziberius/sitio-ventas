using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubgrupoController : Controller
    {

        private readonly ILogger<SubgrupoController> _logger;
        private readonly ISubgrupoService _subgrupoService;


        public SubgrupoController(
            ILogger<SubgrupoController> logger,
            ISubgrupoService subgrupoService
            )
        {
            _logger = logger;
            _subgrupoService = subgrupoService;
        }

        [HttpGet]
        public List<SubgrupoDto> Get()
        {
            return _subgrupoService.GetAll().Result;
        }
    }
}