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
    public class DespesaHospedagemController : ControllerBase//DespesasController<DespesaHospedagem>
    {
        private readonly IDespesasService<DespesaHospedagem> _despesasService;

        public DespesaHospedagemController(IDespesasService<DespesaHospedagem> despesasService)// : base(despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<DespesaHospedagem>> result = await _despesasService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            List<DespesaHospedagemDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(despesas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<DespesaHospedagem> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            DespesaHospedagemDTO despesa = MappingDTOs.ConverterDTO(result.Value);

            return Ok(despesa);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<DespesaHospedagem>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            List<DespesaHospedagemDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(despesas);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarDespesa(DespesaHospedagemDTO despesaDTO)
        {

            DespesaHospedagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaHospedagem> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(result.Error);

            despesa = result.Value;

            return Ok(despesa);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarDespesa(DespesaHospedagemDTO despesaDTO)
        {
            DespesaHospedagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaHospedagem> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(result.Error);

            despesa = result.Value;

            return Ok(despesa);
        }
    }
}
