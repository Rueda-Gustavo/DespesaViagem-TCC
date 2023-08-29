using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemService _viagemService;

        public ViagemController(IViagemService viagemService)
        {
            _viagemService = viagemService;
            //_despesaService = despesaService;
            //_funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasViagens()
        {
            Result<List<ViagemDTO>> viagens = await _viagemService.ObterTodasViagens();

            if (viagens.IsFailure) return BadRequest(viagens.Error);

            return Ok(viagens.Value);
        }

        [HttpGet("{idViagem:int}")]
        public async Task<ActionResult> ObterViagemPorId(int idViagem)
        {
            Result<ViagemDTO> viagem = await _viagemService.ObterViagemPorId(idViagem);

            if (viagem.IsFailure) return BadRequest(viagem.Error);

            return Ok(viagem.Value);
        }


        [HttpGet("/PrestacaoDeContas/{idViagem:int}")]
        public async Task<ActionResult> ObterPrestacaoDeContas(int idViagem)
        {
            var totalDespesas = await _viagemService.ObterPrestacaoDeContas(idViagem);

            if (totalDespesas.IsFailure) return BadRequest(totalDespesas.Error);

            return Ok(totalDespesas.Value);
        }


        [HttpPost("Novo")]
        public async Task<ActionResult> InserirViagem(ViagemDTO viagemDTO)
        {            
            Result<ViagemDTO> viagem = await _viagemService.AdicionarViagem(viagemDTO);

            if (viagem.IsFailure) return BadRequest(viagem.Error);            

            return Ok(viagem.Value) ;
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarViagem(ViagemDTO viagemDTO)
        {
            Result<ViagemDTO> viagem = await _viagemService.AlterarViagem(viagemDTO);

            if (viagem.IsFailure) return BadRequest(viagem.Error);

            return Ok(viagem.Value) ;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult> ObterViagensPorFiltro(string filtro)
        {
            Result<List<ViagemDTO>> viagens = await _viagemService.ObterViagemPorFiltro(filtro);

            if (viagens.IsFailure) return BadRequest(viagens.Error);

            return Ok(viagens.Value);
        }

        [HttpGet("ObterDespesas/{idViagem}")]        
        public async Task<ActionResult> ObterTodasDespesasDaViagem(int idViagem)
        {
            Result<List<DespesaDTO>> result = await _viagemService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            List<DespesaDTO> despesas = result.Value;
            return Ok(despesas);
        }


        [HttpGet("ObterDespesasPorPagina/{idViagem}/{pagina}")]
        public async Task<ActionResult> ObterTodasDespesasPorPagina(int idViagem, int pagina)
        {
            Result<DespesasPorPagina> result = await _viagemService.ObterDespesasPorPagina(idViagem, pagina);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("ObterTodasDespesasPaginadasPorTipo/{idViagem}/{pagina}/{tipoDespesa}")]
        public async Task<ActionResult> ObterTodasDespesasPaginadasPorTipo (int idViagem, int pagina, string tipoDespesa)
        {
            Result<DespesasPorPagina> result = await _viagemService.ObterTodasDespesasPaginadasPorTipo(idViagem, pagina, tipoDespesa);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("ObterDespesasPorCategoria/{idViagem}")]
        
        public async Task<ActionResult> ObterTotalDasDespesasPorCategoria(int idViagem)
        {
            Result<List<DespesaPorCategoria>> result = await _viagemService.ObterTotalDasDespesasPorCategoria(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            List<DespesaPorCategoria> despesas = result.Value;
            return Ok(despesas);
        }

        [HttpGet]
        [Route("status/{statusViagem}")]
        public async Task<ActionResult> ObterViagensPorEstado(StatusViagem statusViagem)
        {
            Result<List<ViagemDTO>> viagens = await _viagemService.ObterViagemPorStatus(statusViagem);

            if (viagens.IsFailure) return BadRequest(viagens.Error);            

            return Ok(viagens.Value);
        }

        [HttpPatch("Iniciar")]
        public async Task<ActionResult> IniciarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.IniciarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Error);            

            return Ok(viagem.Value);
        }

        [HttpPatch("Encerrar")]
        public async Task<ActionResult> EncerrarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.EncerrarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Error);            

            return Ok(viagem.Value);
        }

        [HttpPatch("Cancelar")]
        public async Task<ActionResult> CancelarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.CancelarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Error);

            return Ok(viagem.Value);
        }
    }
}
