using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IGestorService _gestorService;

        public AdminController(IAdminService adminService, IFuncionarioService funcionarioService, IGestorService gestorService)
        {
            _adminService = adminService;
            _funcionarioService = funcionarioService;
            _gestorService = gestorService;
        }

        [HttpGet("ObterUsuarios")]
        public async Task<ActionResult<AdminManutencaoDTO>> ObterUsuarios()
        {
            string idAdmin = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<AdminManutencaoDTO> response = await _adminService.ObterListaUsuarios(int.Parse(idAdmin));            

            if (!response.IsSuccess)
            {
                return BadRequest(new ServiceResponse<AdminManutencaoDTO> { Sucesso = false, Mensagem = response.Error });
            }
            return Ok(new ServiceResponse<AdminManutencaoDTO> { Conteudo = response.Value });
        }

        [HttpGet("lista-funcionarios")]
        public async Task<ActionResult> ObterListaFuncionario()
        {
            string idAdmin = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<FuncionarioDTO>> result = await _adminService.ObterListaFuncionarios(int.Parse(idAdmin));

            if (result.IsFailure)
                return BadRequest(new ServiceResponse<List<FuncionarioDTO>> { Sucesso = false, Mensagem = result.Error });

            return Ok(new ServiceResponse<List<FuncionarioDTO>> { Conteudo = result.Value.ToList() });
        }
    }
}
