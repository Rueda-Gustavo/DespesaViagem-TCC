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
    public class GestorController : UsuariosController<GestorDTO>
    {
        private readonly IGestorService _gestorService;

        public GestorController(IGestorService gestorService) : base(gestorService)
        {
            _gestorService = gestorService;
        }

        [HttpPut]
        public async Task<ActionResult> AlterarGestor(GestorDTO gestor)
        {
            Result<GestorDTO> result = await _gestorService.Alterar(gestor);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<GestorDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<GestorDTO> { Conteudo = result.Value });
        }
        /*
        [HttpPost]
        public async Task<ActionResult> AdicionarGestor(GestorDTO gestor)
        {
            Result<GestorDTO> result = await _gestorService.Adicionar(gestor);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<GestorDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<GestorDTO> { Conteudo = result.Value });
        }
        */
        [HttpGet("lista-funcionarios")]
        public async Task<ActionResult> ObterListaFuncionario()
        {
            string idGestor = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<FuncionarioDTO>> result = await _gestorService.ObterListaFuncionarios(int.Parse(idGestor));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<FuncionarioDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<FuncionarioDTO>> { Conteudo = result.Value.ToList() });
        }
    }
}
