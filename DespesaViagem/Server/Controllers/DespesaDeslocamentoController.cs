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
    public class DespesaDeslocamentoController : DespesasController<DespesaDeslocamento>
    {
        private readonly IDespesasService<DespesaDeslocamento> _despesasService;

        public DespesaDeslocamentoController(IDespesasService<DespesaDeslocamento> despesasService) : base(despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> AdicionarDespesa(DespesaDeslocamentoDTO despesaDTO)
        {

            DespesaDeslocamento despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaDeslocamento> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarDespesa(DespesaDeslocamentoDTO despesaDTO)
        {
            DespesaDeslocamento despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaDeslocamento> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }
    }
}
