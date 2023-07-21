using CSharpFunctionalExtensions;
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

        [HttpGet("Listar")]
        public async Task<ActionResult> ObterTodasDespesas(int idViagem)
        {
            Result<IEnumerable<T>> result = await _despesasService.ObterTodasDespesas(idViagem);

            if (result.IsFailure)
                return BadRequest(result);

            IEnumerable<T> despesas = result.Value.ToList();

            return Ok(despesas);
        }

        [HttpGet("Listar por Id")]
        public async Task<ActionResult> ObterDespesaPorId(string id)
        {
            Result<T> result = await _despesasService.ObterDespesaPorId(id);

            if (result.IsFailure)
                return BadRequest(result);

            T despesa = result.Value;

            return Ok(despesa);
        }

        [HttpGet("Listar por filtro")]
        public async Task<ActionResult> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            Result<IEnumerable<T>> result = await _despesasService.ObterDespesasPorFiltro(filtro, idViagem);

            if (result.IsFailure)
                return BadRequest(result);

            IEnumerable<T> despesa = result.Value.ToList();

            return Ok(despesa);
        } 
    }
}
