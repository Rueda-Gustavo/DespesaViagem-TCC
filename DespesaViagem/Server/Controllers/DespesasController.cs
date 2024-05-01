/*
using CSharpFunctionalExtensions;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController<T> : Controller where T : class
    {
        
        private readonly IDespesasService<T> _despesasService;

        public DespesasController(IDespesasService<T> despesasService)
        {
            _despesasService = despesasService;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<T>> result = await _despesasService.ObterTodasDespesasViagem(idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            IEnumerable<T> despesas = result.Value.ToList();

            return Ok(despesas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<T> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<T>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(result.Error);

            IEnumerable<T> despesa = result.Value.ToList();

            return Ok(despesa);
        } 
        
    }
}
*/