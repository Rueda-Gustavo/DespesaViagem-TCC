using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Services.Services;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController<T> : Controller where T : class
    {
        private readonly IUsuariosService<T> _usuariosService;

        public UsuariosController(IUsuariosService<T> usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            Result<IEnumerable<T>> result = await _usuariosService.ObterTodos();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value.ToList());
        }


        [HttpGet]
        [Route("{filtro}/ObterUsuarioPorFiltro")]
        public async Task<ActionResult> ObtePorFiltro(string filtro)
        {
            Result<IEnumerable<T>> result = await _usuariosService.ObterPorFiltro(filtro);

            if (result.IsFailure)
                return BadRequest(result.Error);

            IEnumerable<T> usuarios = result.Value.ToList();

            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            Result<T> result = await _usuariosService.ObterPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            T usuario = result.Value;

            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<ActionResult> Excluir(int id)
        {
            Result<T> result = await _usuariosService.Remover(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            T usuario = result.Value;

            return Ok(usuario);
        }
    }
}
