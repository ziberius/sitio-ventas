using SitioVentas.Repository.Helpers.Mappers;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitioVentas.Entities.Entities;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace SitioVentas.Services.Services
{
    public class LoginService: ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration, IUsuarioRepository usuarioRepository
        ) {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsuarioDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> Insert(UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> Update(int id, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> ValidateUserAsync(string username, string password)
        {
            var usuario = (await _usuarioRepository.GetAllByExpression(x => x.Identificador == username)).FirstOrDefault();

            if (usuario != null && BCrypt.Net.BCrypt.Verify(password, usuario.Contrasena))
            {
                var token = GenerateJwtToken(usuario);
                UsuarioDto user = new UsuarioDto();
                user.Id = usuario.Id;
                user.Nombre = usuario.Nombre;
                user.Apellido = usuario.Apellido;
                user.Correo = usuario.Correo;
                user.Identificador = usuario.Identificador;

                return new LoginResponseDto
                {
                    Token = token,
                    Usuario = user,
                    Message = "Login exitoso"
                };
            }

            return null;
        }

        private string GenerateJwtToken(Usuario user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Identificador)
                // Agrega más claims según necesites
            }),
                Expires = DateTime.UtcNow.AddHours(2), // Token válido por 8 horas
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
