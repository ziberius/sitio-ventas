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
        private readonly IGrupoService _grupoService;


        public SubgrupoController(
            ILogger<SubgrupoController> logger,
            ISubgrupoService subgrupoService,
            IGrupoService grupoService
            )
        {
            _logger = logger;
            _subgrupoService = subgrupoService;
            _grupoService = grupoService;
        }

        [HttpGet]
        public async Task<List<SubgrupoDto>> Get()
        {
            return await _subgrupoService.GetAll();
        }

        [HttpGet("menu")]
        public async Task<List<MenuGrupoDto>> GetMenu()
        {
            List<MenuGrupoDto> result = new List<MenuGrupoDto>();
            MenuGrupoDto menuGrupo;
            MenuSubgrupoDto menuSubgrupo;
            var grupos = await _grupoService.GetAll();
            foreach (var grupo in grupos)
            {
                menuGrupo = new MenuGrupoDto();
                menuGrupo.menuSubgrupo = new List<MenuSubgrupoDto>();
                menuGrupo.Nombre = grupo.Nombre;
                menuGrupo.Id = grupo.Id;
                menuGrupo.Codigo = grupo.Codigo;

                var subgrupos = await _subgrupoService.GetByMenuId(menuGrupo.Id);
                foreach(var subgrupo in subgrupos)
                {
                    menuSubgrupo = new MenuSubgrupoDto();
                    menuSubgrupo.Nombre = subgrupo.Nombre;
                    menuSubgrupo.Id = subgrupo.Id;
                    menuSubgrupo.Codigo = subgrupo.Codigo;
                    menuGrupo.menuSubgrupo.Add(menuSubgrupo);
                }
                result.Add(menuGrupo);
            }
            return result;
        }
    }
}