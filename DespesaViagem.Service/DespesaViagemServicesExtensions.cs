﻿using DespesaViagem.Services.Interfaces;
using DespesaViagem.Services.Services;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.Extensions.DependencyInjection;


namespace DespesaViagem.Services
{
    public static class DespesaViagemServiceExtensions
    {
        public static IServiceCollection AddDespesaViagemService(this IServiceCollection service)
        {
          
            service.AddScoped<IEnderecoService, EnderecoService>();
            service.AddScoped<IGestorService, GestorService>();
            service.AddScoped<IFuncionarioService, FuncionarioService>();
            service.AddScoped<IViagemService, ViagemService>();
            service.AddScoped<IDespesaService, DespesaService>();
            service.AddScoped<IDespesasService<DespesaHospedagem>, DespesaHospedagemService>();
            service.AddScoped<IDespesasService<DespesaAlimentacao>, DespesaAlimentacaoService>();
            service.AddScoped<IDespesasService<DespesaDeslocamento>, DespesaDeslocamentoService>();
            service.AddScoped<IDespesasService<DespesaPassagem>, DespesaPassagemService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IAdminService, AdminService>();
            service.AddScoped<IDepartamentoService, DepartamentoService>();
            return service;
        }
    }
}
