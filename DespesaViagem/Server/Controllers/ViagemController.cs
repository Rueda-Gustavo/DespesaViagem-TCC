﻿using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using DespesaViagem.Shared.Models.Viagens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<List<ViagemDTO>> result = await _viagemService.ObterTodasViagens(int.Parse(idUsuario)); //Ideia para que seja possível mostrar todas as viagens dos funcionários do gestor ou somente as viagens do próprio funcionário

            //Result<List<ViagemDTO>> viagens = await _viagemService.ObterTodasViagens();

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }

        [HttpGet("ObterViagensPorPagina/{pagina}")]
        public async Task<ActionResult> ObterTodasDespesasPorPagina(int pagina)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagensPorPagina> result = await _viagemService.ObterViagensPaginadas(int.Parse(idUsuario), pagina);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }

        [HttpGet("{idViagem:int}")]
        public async Task<ActionResult> ObterViagemPorId(int idViagem)
        {
            Result<ViagemDTO> result = await _viagemService.ObterViagemPorId(idViagem);

            if (result.IsFailure) return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }
        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        [HttpGet("PorFuncionario/{idFuncionario}")]
        public async Task<ActionResult> ObterViagemPorFuncionario(int idFuncionario)
        {
            Result<List<ViagemDTO>> result = await _viagemService.ObterViagensPorFuncionario(idFuncionario);

            if (result.IsFailure) return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }
        //-------------------------------------------------------------------------------------------------------------*/

        [HttpGet("PorFuncionario/{idFuncionario}/{pagina}")]
        public async Task<ActionResult> ObterViagemPorFuncionario(int idFuncionario, int pagina)
        {
            Result<ViagensPorPagina> result = await _viagemService.ObterViagensPorFuncionario(idFuncionario, pagina);

            if (result.IsFailure) return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }

        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        [HttpGet("PorDepartamento/{idDepartamento:int}")]
        public async Task<ActionResult> ObterViagemPorDepartamento(int idDepartamento)
        {
            Result<List<ViagemDTO>> result = await _viagemService.ObterViagensPorDepartamento(idDepartamento);

            if (result.IsFailure) return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }
        //-------------------------------------------------------------------------------------------------------------*/

        [HttpGet("PorDepartamento/{idDepartamento:int}/{pagina:int}")]
        public async Task<ActionResult> ObterViagemPorDepartamento(int idDepartamento, int pagina)
        {
            Result<ViagensPorPagina> result = await _viagemService.ObterViagensPorDepartamento(idDepartamento, pagina);

            if (result.IsFailure) return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }

        [HttpGet("PrestacaoDeContas/{idViagem:int}")]
        public async Task<ActionResult> ObterPrestacaoDeContas(int idViagem)
        {
            Result<decimal> result = await _viagemService.ObterPrestacaoDeContas(idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<decimal> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<decimal> { Conteudo = result.Value });
        }

        [HttpPost]
        public async Task<ActionResult> InserirViagem(ViagemDTO viagemDTO)
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO> result = await _viagemService.AdicionarViagem(viagemDTO, int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarViagem([FromBody] ViagemDTO viagemDTO)
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO> result = await _viagemService.AlterarViagem(viagemDTO, int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            //return Ok(result.Value);

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }

        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        [HttpGet("{filtro}")]
        public async Task<ActionResult> ObterViagensPorFiltro(string filtro)
        {
            Result<List<ViagemDTO>> result = await _viagemService.ObterViagensPorFiltro(filtro);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }
        //-------------------------------------------------------------------------------------------------------------*/


        [HttpGet("{filtro}/{pagina:int}")]
        public async Task<ActionResult> ObterViagensPorFiltro(string filtro, int pagina)
        {
            Result<ViagensPorPagina> result = await _viagemService.ObterViagensPorFiltro(filtro, pagina);

            if (result.IsFailure) return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }

        [HttpGet("ObterDespesas/{idViagem}")]
        public async Task<ActionResult> ObterTodasDespesasDaViagem(int idViagem)
        {
            Result<List<DespesaDTO>> result = await _viagemService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<DespesaDTO>> { Sucesso = false, Mensagem = result.Error });

            //List<DespesaDTO> despesas = result.Value;

            return Ok(new ServiceResponse<List<DespesaDTO>> { Conteudo = result.Value });
        }


        [HttpGet("ObterDespesasPorPagina/{idViagem}/{pagina}")]
        public async Task<ActionResult> ObterTodasDespesasPorPagina(int idViagem, int pagina)
        {
            Result<DespesasPorPagina> result = await _viagemService.ObterDespesasPorPagina(idViagem, pagina);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesasPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<DespesasPorPagina> { Conteudo = result.Value });
        }

        [HttpGet("ObterTodasDespesasPaginadasPorTipo/{idViagem}/{pagina}/{tipoDespesa}")]
        public async Task<ActionResult> ObterTodasDespesasPaginadasPorTipo(int idViagem, int pagina, string tipoDespesa)
        {
            Result<DespesasPorPagina> result = await _viagemService.ObterTodasDespesasPaginadasPorTipo(idViagem, pagina, tipoDespesa);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<DespesasPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<DespesasPorPagina> { Conteudo = result.Value });
        }

        [HttpGet("ObterDespesasPorCategoria/{idViagem}")]

        public async Task<ActionResult> ObterTotalDasDespesasPorCategoria(int idViagem)
        {
            Result<List<DespesaPorCategoria>> result = await _viagemService.ObterTotalDasDespesasPorCategoria(idViagem);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<DespesaPorCategoria>> { Sucesso = false, Mensagem = result.Error });

            //List<DespesaPorCategoria> despesas = result.Value;

            return Ok(new ServiceResponse<List<DespesaPorCategoria>> { Conteudo = result.Value });
        }
        /*
        [HttpGet]
        [Route("status/{statusViagem}")]
        public async Task<ActionResult> ObterViagensPorStatus(StatusViagem statusViagem)
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<List<ViagemDTO>> result = await _viagemService.ObterViagemPorStatus(statusViagem, int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }
        */
        /*
        [HttpGet("status/{statusViagem}/TodasViagens")]
        public async Task<ActionResult> ObterTodasViagensPorStatus(StatusViagem statusViagem)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<List<ViagemDTO>> result = await _viagemService.ObterTodasViagensStatus(statusViagem, int.Parse(idUsuario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }
        */
        [HttpGet("status/{statusViagem}/TodasViagens/{pagina:int}")]
        public async Task<ActionResult> ObterTodasViagensPorStatus(StatusViagem statusViagem, int pagina)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagensPorPagina> result = await _viagemService.ObterTodasViagensPorStatus(statusViagem, int.Parse(idUsuario), pagina);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }

        [HttpPost("listaStatus/TodasViagens")]
        public async Task<ActionResult> ObterTodasViagensPorStatus([FromBody] List<StatusViagem> statusViagem)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<List<ViagemDTO>> result = await _viagemService.ObterTodasViagensStatus(statusViagem, int.Parse(idUsuario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<ViagemDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<ViagemDTO>> { Conteudo = result.Value });
        }

        /*Trecho antigo, inutilizado------------------------------------------------------------------------------------
        [HttpGet("status/{statusViagem}/{pagina:int}")]
        public async Task<ActionResult> ObterViagensPorStatus(StatusViagem statusViagem, int pagina)
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagensPorPagina> result = await _viagemService.ObterViagemPorStatus(statusViagem, int.Parse(idUsuario), pagina);

            if (result.IsFailure) return BadRequest(new ServiceResponse<ViagensPorPagina> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagensPorPagina> { Conteudo = result.Value });
        }
        //-------------------------------------------------------------------------------------------------------------*/

        [HttpGet("ViagemAbertaOuEmAndamento")]        
        public async Task<ActionResult> ObterViagemAbertaOuEmAndamento()
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO?> result = await _viagemService.ObterViagemAbertaOuEmAndamento(int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }

        [HttpPatch("Iniciar")]
        public async Task<ActionResult> IniciarViagem()
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO> result = await _viagemService.IniciarViagem(int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }

        [HttpPatch("Encerrar")]
        public async Task<ActionResult> EncerrarViagem()
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO> result = await _viagemService.EncerrarViagem(int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }

        [HttpPatch("Cancelar")]
        public async Task<ActionResult> CancelarViagem()
        {
            string idFuncionario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<ViagemDTO> result = await _viagemService.CancelarViagem(int.Parse(idFuncionario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<ViagemDTO> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<ViagemDTO> { Conteudo = result.Value });
        }
    }
}
