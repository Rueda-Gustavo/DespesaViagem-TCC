using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Viagens;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodosDepartamentos()
        {
            string idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<Departamento>> result = await _departamentoService.ObterDepartamentos(int.Parse(idUsuario));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<IEnumerable<Departamento>> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<IEnumerable<Departamento>> { Conteudo = result.Value });
        }

        [HttpGet("{idDepartamento:int}")]
        public async Task<ActionResult> ObterDepartamento(int idDepartamento)
        {           
            Result<Departamento> result = await _departamentoService.ObterDepartamento(idDepartamento);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }

        [HttpGet("{descricao}")]
        public async Task<ActionResult> ObterDepartamento(string descricao)
        {
            Result<Departamento> result = await _departamentoService.ObterDepartamento(descricao);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });


            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }


        [HttpPost]
        public async Task<ActionResult> InserirDepartamento([FromBody] string descricao)
        {            
            Result<Departamento> result = await _departamentoService.AdicionarDepartamento(descricao);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }

        [HttpPatch("ativar")]
        public async Task<ActionResult> AtivarDepartamento([FromBody] int idDepartamento)
        {
            Result<Departamento> result = await _departamentoService.AtivarDepartamento(idDepartamento);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }


        [HttpPatch("desativar")]
        public async Task<ActionResult> DesativarDepartamento([FromBody] int idDepartamento)
        {
            Result<Departamento> result = await _departamentoService.DesativarDepartamento(idDepartamento);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarViagem([FromBody] Departamento departamento)
        {
            Result<Departamento> result = await _departamentoService.AlterarDepartamento(departamento);

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<Departamento> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<Departamento> { Conteudo = result.Value });
        }
    }
}
