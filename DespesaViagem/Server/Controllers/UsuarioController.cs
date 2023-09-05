using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            //_configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginUsuario usuario)
        {
            var response = await _usuarioService.Login(usuario.Username, usuario.Password);
            ServiceResponse<string> result;
            if (!response.IsSuccess)
            {
                result = new ServiceResponse<string>
                {
                    Mensagem = response.Error,
                    Sucesso = false
                };
                return BadRequest(result);
            }

            result = new ServiceResponse<string>
            {
                Conteudo = response.Value                
            };

            return Ok(result);
        }

        [HttpPost("trocar-senha"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> TrocarSenha([FromBody] string newPassword)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";
            
            Result<bool> response = await _usuarioService.TrocarSenha(int.Parse(idUsuario), newPassword);            

            if (!response.IsSuccess)
            {
                return BadRequest(new ServiceResponse<bool> { Conteudo = false, Mensagem = response.Error });
            }
            return Ok(new ServiceResponse<bool> { Conteudo = true, Mensagem = "Senha alterada com sucesso." });
        }
    }
}
