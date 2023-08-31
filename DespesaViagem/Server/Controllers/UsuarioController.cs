using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Login(LoginUsuario usuario)
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
    }
}
