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
    public class DespesaHospedagemController : DespesasController<DespesaHospedagem>
    {
        private readonly IDespesasService<DespesaHospedagem> _despesasService;

        public DespesaHospedagemController(IDespesasService<DespesaHospedagem> despesasService) : base(despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> AdicionarDespesa(DespesaHospedagemDTO despesaDTO)
        {

            DespesaHospedagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaHospedagem> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarDespesa(DespesaHospedagemDTO despesaDTO)
        {
            DespesaHospedagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaHospedagem> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }
    }
}
