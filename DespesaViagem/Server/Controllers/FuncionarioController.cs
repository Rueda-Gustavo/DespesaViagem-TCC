using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }


        [HttpGet]
        public async Task<ActionResult> ObterTodosFuncionarios()
        {
            Result<IEnumerable<Funcionario>> result = await _funcionarioService.ObterTodosFuncionarios();

            if (result.IsFailure)
                return BadRequest(result.Value);

            return Ok(result.Value.ToList());
        }


        [HttpGet]
        [Route("{filtro}/ObterFuncionarioPorFiltro")]
        public async Task<ActionResult> ObterFuncionarioPorFiltro(string filtro)
        {
            Result<IEnumerable<Funcionario>> result = await _funcionarioService.ObterFuncionarioPorFiltro(filtro);

            if (result.IsFailure)
                return BadRequest(result.Value);

            IEnumerable<Funcionario> funcionarios = result.Value.ToList();

            return Ok(funcionarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterFuncionarioPorId(int id)
        {
            Result<Funcionario> result = await _funcionarioService.ObterFuncionarioPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Value);

            Funcionario funcionario = result.Value;

            return Ok(funcionario);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AlterarFuncionario(Funcionario funcionario)
        {
            Result<Funcionario> result = await _funcionarioService.AlterarFuncionario(funcionario);

            if (result.IsFailure)
                return BadRequest(result.Value);

            funcionario = result.Value;

            return Ok(funcionario);
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> AdicionarFuncionario(Funcionario funcionario)
        {
            Result<Funcionario> result = await _funcionarioService.AdicionarFuncionario(funcionario);

            if (result.IsFailure)
                return BadRequest(result.Value);

            funcionario = result.Value;

            return Ok(funcionario);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> ExcluirFuncionario(int id)
        {
            Result<Funcionario> result = await _funcionarioService.RemoverFuncionario(id);

            if (result.IsFailure)
                return BadRequest(result.Value);

            Funcionario funcionario = result.Value;

            return Ok(funcionario);
        }
        
    }
}
