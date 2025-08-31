using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Data.Respositorio;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionPrestamosBancoService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _iusuario;
        private readonly IConfiguration _config;

        public LoginController(IUsuario usuarioRepository, IConfiguration config)
        {
            _iusuario = usuarioRepository;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _iusuario.ObtenerPorUsername(request.Username);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuario no encontrado" });
            }

            if (usuario.PasswordHash != request.Password)
            {
                return Unauthorized(new { message = "Contraseña incorrecta" });
            }

            var token = GenerarToken(usuario);

            return Ok(new LoginResponse
            {
                Token = token,
                Username = usuario.Username
            });
        }

        private string GenerarToken(Models.Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim("IdUsuario", usuario.IdUsuario.ToString())
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
