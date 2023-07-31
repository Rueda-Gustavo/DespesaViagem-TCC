using CSharpFunctionalExtensions;
using DespesaViagem.Client.Pages;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

            if (viagens.IsFailure) return BadRequest(viagens.Value);

            return Ok(viagens.Value);
        }

        [HttpGet("{idViagem:int}")]
        public async Task<ActionResult> ObterViagemPorId(int idViagem)
        {
            Result<ViagemDTO> viagem = await _viagemService.ObterViagemPorId(idViagem);

            if (viagem.IsFailure) return BadRequest(viagem.Value);

            return Ok(viagem.Value);
        }

        [HttpPost("Novo")]
        public async Task<ActionResult> InserirViagem(ViagemDTO viagemDTO)
        {
            Result<ViagemDTO> viagem = await _viagemService.AdicionarViagem(viagemDTO);

            if (viagem.IsFailure) return BadRequest(viagem.Value);            

            return Ok(viagem.Value) ;
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarViagem(ViagemDTO viagemDTO)
        {
            Result<ViagemDTO> viagem = await _viagemService.AlterarViagem(viagemDTO);

            if (viagem.IsFailure) return BadRequest(viagem.Value);            

            return Ok(viagem);
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult> ObterViagensPorFiltro(string filtro)
        {
            Result<List<ViagemDTO>> viagens = await _viagemService.ObterViagemPorFiltro(filtro);

            if (viagens.IsFailure) return BadRequest(viagens.Value);

            return Ok(viagens.Value);
        }

        [HttpGet]
        [Route("ObterDespesas/{idViagem}")]
        public async Task<ActionResult> ObterTodasDespesasDaViagem(int idViagem)
        {
            Result<List<DespesaDTO>> result = await _viagemService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Value);

            List<DespesaDTO> despesas = result.Value;
            return Ok(despesas);
        }

        [HttpGet]
        [Route("status/{statusViagem}")]
        public async Task<ActionResult> ObterViagensPorEstado(StatusViagem statusViagem)
        {
            Result<List<ViagemDTO>> viagens = await _viagemService.ObterViagemPorStatus(statusViagem);

            if (viagens.IsFailure) return BadRequest(viagens.Value);            

            return Ok(viagens.Value);
        }

        [HttpPatch("Iniciar")]
        public async Task<ActionResult> IniciarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.IniciarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Value);            

            return Ok(viagem);
        }

        [HttpPatch("Encerrar")]
        public async Task<ActionResult> EncerrarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.EncerrarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Value);            

            return Ok(viagem.Value);
        }

        [HttpPatch("Cancelar")]
        public async Task<ActionResult> CancelarViagem()
        {
            Result<ViagemDTO> viagem = await _viagemService.CancelarViagem();

            if (viagem.IsFailure) return BadRequest(viagem.Value);

            return Ok(viagem.Value);
        }
    }
}
