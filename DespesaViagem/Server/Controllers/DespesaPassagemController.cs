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
    public class DespesaPassagemController : DespesasController<DespesaPassagem>
    {
        private readonly IDespesasService<DespesaPassagem> _despesasService;

        public DespesaPassagemController(IDespesasService<DespesaPassagem> despesasService) : base(despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> AdicionarDespesa(DespesaPassagemDTO despesaDTO)
        {

            DespesaPassagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaPassagem> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarDespesa(DespesaPassagemDTO despesaDTO)
        {
            DespesaPassagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaPassagem> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest();

            despesa = result.Value;

            return Ok(despesa);
        }
    }
}
