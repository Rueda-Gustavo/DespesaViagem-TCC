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
                return BadRequest(result.Error);

            funcionario = result.Value;

            return Ok(funcionario);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> AdicionarFuncionario(CadastroUsuario request)
        {
            Result<Funcionario> result = await _funcionarioService.Adicionar(
                new Funcionario
                {
                    NomeCompleto = request.NomeCompleto,
                    CPF = request.CPF,
                    Matricula = request.Matricula ?? string.Empty,                    
                    Username = request.Username
                }, request.Password);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Funcionario> { Message = result.Error, Sucesso = false });            

            return Ok(new ServiceResponse<Funcionario> { Conteudo = result.Value });
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