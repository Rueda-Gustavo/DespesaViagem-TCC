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
    public class DespesaAlimentacaoController : DespesasController<DespesaAlimentacao>
    {
        private readonly IDespesasService<DespesaAlimentacao> _despesasService;

        public DespesaAlimentacaoController(IDespesasService<DespesaAlimentacao> despesasService) : base(despesasService)
        {
            _despesasService = despesasService;
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
