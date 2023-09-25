using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestorController : UsuariosController<Gestor>
    {
        private readonly IGestorService _gestorService;

        public GestorController(IGestorService gestorService) : base(gestorService)
        {
            _gestorService = gestorService;
        }

        [HttpPut]
        public async Task<ActionResult> AlterarGestor(GestorDTO gestor)
        {
            Result<Gestor> result = await _gestorService.Alterar(gestor);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Gestor> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Gestor> { Conteudo = result.Value });
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarGestor(Gestor gestor)
        {
            Result<Gestor> result = await _gestorService.Adicionar(gestor);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Gestor> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Gestor> { Conteudo = result.Value });
        }

        [HttpGet("lista-funcionarios")]
        public async Task<ActionResult> ObterListaFuncionario()
        {
            string idGestor = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<Funcionario>> result = await _gestorService.ObterListaFuncionarios(int.Parse(idGestor));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<Funcionario>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<IEnumerable<Funcionario>> { Conteudo = result.Value });

        }
    }
}
