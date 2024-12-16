using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SitioVentas.Dto.Dto;
using SitioVentas.Services.IServices;

namespace SitioVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginService _loginService;

        public AuthController(ILogger<AuthController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _loginService.ValidateUserAsync(request.Username, request.Password);

            if (response != null)
            {
                return Ok(new
                {
                    token = response.Token,
                    usuario = response.Usuario,
                    message = response.Message
                });
            }

            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }
    }
}
