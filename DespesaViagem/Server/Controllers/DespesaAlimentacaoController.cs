using CSharpFunctionalExtensions;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAlimentacaoController : ControllerBase
    {
        private readonly IDespesasService<DespesaAlimentacao> _despesasService;

        public DespesaAlimentacaoController(IDespesasService<DespesaAlimentacao> despesasService)
        {
            _despesasService = despesasService;
        }

                [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<DespesaAlimentacao>> result = await _despesasService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result);

            List<DespesaAlimentacaoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(despesas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<DespesaAlimentacao> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Value);

            DespesaAlimentacaoDTO despesa = MappingDTOs.ConverterDTO(result.Value);

            return Ok(despesa);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<DespesaAlimentacao>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(result);

            List<DespesaAlimentacaoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(despesas);
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> AdicionarDespesa(DespesaAlimentacaoDTO despesaDTO)
        {

            DespesaAlimentacao despesa = MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaAlimentacao> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarDespesa(DespesaAlimentacaoDTO despesaDTO)
        {
            DespesaAlimentacao despesa = MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaAlimentacao> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }
    }
}
