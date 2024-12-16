using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Dto.Dto
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
        public required UsuarioDto Usuario { get; set; }
        public required string Message { get; set; }
    }
}
