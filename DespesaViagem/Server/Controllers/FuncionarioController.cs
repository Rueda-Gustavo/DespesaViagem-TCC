using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : UsuariosController<Funcionario>
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService) : base(funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPut]
        public async Task<ActionResult> AlterarFuncionario(Funcionario funcionario)
        {
            Result<Funcionario> result = await _funcionarioService.Alterar(funcionario);

            if (result.IsFailure)
                return BadRequest(result.Value);

            funcionario = result.Value;

            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarFuncionario(Funcionario funcionario)
        {
            Result<Funcionario> result = await _funcionarioService.Adicionar(funcionario);

            if (result.IsFailure)
                return BadRequest(result.Value);

            funcionario = result.Value;

            return Ok(funcionario);
        }        
    }
}

/*
 {
  "id": 0,
  "nome": "Gustavo",
  "sobrenome": "Rueda",
  "username": "gustavo.rueda",
  "tipoDeUsuario": 0,
  "passwordHash": "senhagenerica1",
  "matricula": "ASD16A5SD1",
  "cpf": "987.654.321-12",
  "idGestor": 2,
  "idViagem": 0
}
*/