using DespesaViagem.Shared.DTOs.Security;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Usuario usuario = new();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Usuario> Register(UsuarioDTO request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            usuario.Username = request.Username;
            usuario.PasswordHash = passwordHash;

            return Ok(usuario);
        }

        [HttpPost("login")]
        public ActionResult<Usuario> Login(UsuarioDTO request)
        {
            if (usuario.Username != request.Username)
            {
                return BadRequest("Usuário não encontrado.");
            }
            //Para o projeto final deixar as duas verificações no mesmo if e deixar
            //o alerta junto, para evitar problemas de segurança
            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            {
                return BadRequest("Senha incorreta.");
            }

            string token = CreateToken(usuario);


            return Ok(token);
        }


        private string CreateToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Username)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
