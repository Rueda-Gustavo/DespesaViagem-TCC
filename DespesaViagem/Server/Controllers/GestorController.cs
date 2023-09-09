﻿using CSharpFunctionalExtensions;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;

namespace DespesaViagem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestorController : UsuariosController<Gestor>
    {
        private readonly IGestorService _gestorService;

        public GestorController(IGestorService gestorService) : base(gestorService)
        {
            _gestorService = gestorService;
        }
        [HttpPut]
        public async Task<ActionResult> AlterarGestor(Gestor gestor)
        {
            Result<Gestor> result = await _gestorService.Alterar(gestor);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarGestor(Gestor gestor)
        {
            Result<Gestor> result = await _gestorService.Adicionar(gestor);

            if (result.IsFailure)
                return BadRequest(result.Error);            

            return Ok(result.Value);
        }

        [HttpGet("lista-funcionarios")]
        public async Task<ActionResult> ObterListaFuncionario()
        {
            string idGestor = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";

            Result<IEnumerable<Funcionario>> result = await _gestorService.ObterListaFuncionarios(int.Parse(idGestor));

            if (result.IsFailure)
                return BadRequest(result.Error);            

            return Ok(result.Value);

        }
    }
}
