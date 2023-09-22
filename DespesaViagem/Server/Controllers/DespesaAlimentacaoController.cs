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
                return BadRequest(new ServiceResponse<IEnumerable<DespesaAlimentacaoDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaAlimentacaoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(new ServiceResponse<List<DespesaAlimentacaoDTO>> { Conteudo = despesas });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<DespesaAlimentacao> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaAlimentacaoDTO> { Sucesso = false, Mensagem = result.Error });

            DespesaAlimentacaoDTO despesa = MappingDTOs.ConverterDTO(result.Value);

            return Ok(new ServiceResponse<DespesaAlimentacaoDTO> { Conteudo = despesa });
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<DespesaAlimentacao>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<DespesaAlimentacaoDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaAlimentacaoDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(new ServiceResponse<List<DespesaAlimentacaoDTO>> { Conteudo = despesas });
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarDespesa(DespesaAlimentacaoDTO despesaDTO)
        {

            DespesaAlimentacao despesa = MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaAlimentacao> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaAlimentacao> { Sucesso = false, Mensagem = result.Error });

            //despesa = result.Value;

            return Ok(new ServiceResponse<DespesaAlimentacao> { Conteudo = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarDespesa(DespesaAlimentacaoDTO despesaDTO)
        {
            DespesaAlimentacao despesa = MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaAlimentacao> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaAlimentacao> { Sucesso = false, Mensagem = result.Error });

            //despesa = result.Value;

            return Ok(new ServiceResponse<DespesaAlimentacao> { Conteudo = result.Value });
        }
    }
}
