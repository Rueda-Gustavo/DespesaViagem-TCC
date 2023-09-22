using CSharpFunctionalExtensions;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaDeslocamentoController : ControllerBase
    {
        private readonly IDespesasService<DespesaDeslocamento> _despesasService;

        public DespesaDeslocamentoController(IDespesasService<DespesaDeslocamento> despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<DespesaDeslocamento>> result = await _despesasService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<DespesaDeslocamentoDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaDeslocamentoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(new ServiceResponse<List<DespesaDeslocamentoDTO>> { Conteudo = despesas });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<DespesaDeslocamento> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaDeslocamentoDTO> { Sucesso = false, Mensagem = result.Error });

            DespesaDeslocamentoDTO despesa = MappingDTOs.ConverterDTO(result.Value);

            return Ok(new ServiceResponse<DespesaDeslocamentoDTO> { Conteudo = despesa });
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<DespesaDeslocamento>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<DespesaDeslocamentoDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaDeslocamentoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(new ServiceResponse<List<DespesaDeslocamentoDTO>> { Conteudo = despesas });
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarDespesa(DespesaDeslocamentoDTO despesaDTO)
        {

            DespesaDeslocamento despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaDeslocamento> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaDeslocamento> { Sucesso = false, Mensagem = result.Error });

            //despesa = result.Value;

            return Ok(new ServiceResponse<DespesaDeslocamento> { Conteudo = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarDespesa(DespesaDeslocamentoDTO despesaDTO)
        {
            DespesaDeslocamento despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaDeslocamento> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaDeslocamento> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<DespesaDeslocamento> { Conteudo = result.Value });
        }
    }
}
