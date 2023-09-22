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
    public class DespesaPassagemController : ControllerBase
    {
        private readonly IDespesasService<DespesaPassagem> _despesasService;

        public DespesaPassagemController(IDespesasService<DespesaPassagem> despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<DespesaPassagem>> result = await _despesasService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<DespesaPassagemDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaPassagemDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());
             
            return Ok(new ServiceResponse<List<DespesaPassagemDTO>> { Conteudo = despesas });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<DespesaPassagem> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaPassagemDTO> { Sucesso = false, Mensagem = result.Error });

            DespesaPassagemDTO despesa = MappingDTOs.ConverterDTO(result.Value);

            return Ok(new ServiceResponse<DespesaPassagemDTO> { Conteudo = despesa });
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<DespesaPassagem>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<DespesaPassagemDTO>> { Sucesso = false, Mensagem = result.Error });

            List<DespesaPassagemDTO> despesas = MappingDTOs.ConverterDTO(result.Value.ToList());

            return Ok(new ServiceResponse<List<DespesaPassagemDTO>> { Conteudo = despesas });
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarDespesa(DespesaPassagemDTO despesaDTO)
        {

            DespesaPassagem despesa = MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaPassagem> result = await _despesasService.AdicionarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaPassagem> { Sucesso = false, Mensagem = result.Error });

            despesa = result.Value;

            return Ok(new ServiceResponse<DespesaPassagem> { Conteudo = despesa });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarDespesa(DespesaPassagemDTO despesaDTO)
        {
            DespesaPassagem despesa= MappingDTOs.ConverterDTO(despesaDTO);

            Result<DespesaPassagem> result = await _despesasService.AlterarDespesa(despesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesaPassagem> { Sucesso = false, Mensagem = result.Error });

            despesa = result.Value;

            return Ok(new ServiceResponse<DespesaPassagem> { Conteudo = despesa });

        }
    }
}
