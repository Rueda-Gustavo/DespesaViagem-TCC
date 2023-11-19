using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : Controller
    {
        private readonly IDespesaService _despesaService;

        public DespesaController(IDespesaService despesaService)
        {
            _despesaService = despesaService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas()
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<DespesaDTO>> result = await _despesaService.ObterTodasDespesas(int.Parse(idUsuario));

            //Result<List<ViagemDTO>> viagens = await _viagemService.ObterTodasViagens();

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<DespesaDTO>> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<IEnumerable<DespesaDTO>> { Conteudo = result.Value });
        }
    }
}
