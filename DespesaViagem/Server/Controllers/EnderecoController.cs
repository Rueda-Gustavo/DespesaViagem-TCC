using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodosEnderecos()
        {
            Result<IEnumerable<Endereco>> result = await _enderecoService.ObterTodosEnderecos();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value.ToList());
        }

        [HttpGet]
        [Route("{filtro}/ObterEnderecoPorFiltro")]
        public async Task<ActionResult> ObterEnderecoPorFiltro(string filtro)
        {
            Result<IEnumerable<Endereco>> result = await _enderecoService.ObterEnderecoPorFiltro(filtro);

            if (result.IsFailure)
                return BadRequest(result.Error);

            IEnumerable<Endereco> enderecos = result.Value.ToList();

            return Ok(enderecos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterEnderecoPorId(int id)
        {
            Result<Endereco> result = await _enderecoService.ObterEnderecoPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            Endereco endereco = result.Value;

            return Ok(endereco);
        }

        [HttpPut]
        public async Task<ActionResult> AlterarEndereco([FromBody] Endereco endereco)
        {
            Result<Endereco> result = await _enderecoService.AlterarEndereco(endereco);

            if (result.IsFailure)
                return BadRequest(result.Error);

            endereco = result.Value;

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarEndereco([FromBody] Endereco endereco)
        {
            Result<Endereco> result = await _enderecoService.AdicionarEndereco(endereco);

            if (result.IsFailure)
                return BadRequest(result.Error);

            endereco = result.Value;

            return Ok(endereco);
        }

        [HttpDelete]
        public async Task<ActionResult> ExcluirEndereco(int id)
        {
            Result<Endereco> result = await _enderecoService.RemoverEndereco(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            Endereco endereco = result.Value;

            return Ok(endereco);
        }
    }
}
