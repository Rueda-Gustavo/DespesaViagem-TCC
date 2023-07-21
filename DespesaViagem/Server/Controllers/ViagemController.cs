using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
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
            Result<IEnumerable<Viagem>> result = await _viagemService.ObterTodasViagens();

            if (result.IsFailure)
                return BadRequest(result.Value);

            IEnumerable<Viagem> viagens = result.Value.ToList();

            return Ok(viagens);
        }

        [HttpGet("{id:int}")]        
        public async Task<ActionResult> ObterViagemPorId(int idViagem)
        {
            Result<Viagem> result = await _viagemService.ObterViagemPorId(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Value);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> InserirViagem(ViagemDTO viagemDTO)
        {                      
            Result<Viagem> result = await _viagemService.AdicionarViagem(viagemDTO);

            if (result.IsFailure)
                return BadRequest(result.Value);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarViagem(ViagemDTO viagemDTO)
        {
            Result<Viagem> result = await _viagemService.AlterarViagem(viagemDTO);

            if (result.IsFailure)
                return BadRequest(result.Value);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }


        [HttpGet("{filtro}")]
        public async Task<ActionResult> ObterViagensPorFiltro(string filtro)
        {
            Result<IEnumerable<Viagem>> result = await _viagemService.ObterViagemPorFiltro(filtro);

            if (result.IsFailure)
                return BadRequest(result);

            IEnumerable<Viagem> viagens = result.Value.ToList();

            return Ok(viagens);
        }

        [HttpGet]
        [Route("{id}/ObterDespesas")]
        public async Task<ActionResult> ObterTodasDespesasDaViagem(int idViagem)
        {
            Result<IEnumerable<Despesa>> result = await _viagemService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result);

            List<Despesa> despesas = result.Value.ToList();
            return Ok(despesas);
        }

        [HttpGet]
        [Route("status/{statusViagem}")]
        public async Task<ActionResult> ObterViagensPorEstado(StatusViagem statusViagem)
        {
            Result<IEnumerable<Viagem>> result = await _viagemService.ObterViagemPorStatus(statusViagem);

            if (result.IsFailure)
                return BadRequest(result);

            IEnumerable<Viagem> viagens = result.Value.ToList();

            return Ok(viagens);
        }

        [HttpPatch("Iniciar")]
        public async Task<ActionResult> IniciarViagem()
        {
            Result<Viagem> result = await _viagemService.IniciarViagem();

            if (result.IsFailure)
                return BadRequest(result);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }

        [HttpPatch("Encerrar")]
        public async Task<ActionResult> EncerrarViagem()
        {                                           
            Result<Viagem> result = await _viagemService.EncerrarViagem();

            if (result.IsFailure)
                return BadRequest(result);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }

        [HttpPatch("Cancelar")]        
        public async Task<ActionResult> CancelarViagem()
        {
            Result<Viagem> result = await _viagemService.CancelarViagem();

            if (result.IsFailure)
                return BadRequest(result);

            Viagem viagem = result.Value;

            return Ok(viagem);
        }
    }
}
