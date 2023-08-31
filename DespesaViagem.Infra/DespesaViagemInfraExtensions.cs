using DespesaViagem.Infra.Database;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DespesaViagem.Infra
{
    public static class DespesaViagemInfraExtensions
    {
        public static IServiceCollection AddDespesaViagemInfra(this IServiceCollection service, IConfiguration configuration)
        {
            var a = configuration.GetConnectionString("SqlServer");
            service.AddDbContext<DespesaViagemContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            service.AddScoped<IEnderecoRepository, EnderecoRepository>();
            service.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            service.AddScoped<IGestorRepository, GestorRepository>();
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IViagemRepository, ViagemRepository>();
            service.AddScoped<IDespesaRepository, DespesaRepository>();
            service.AddScoped<IDespesasRepository<DespesaHospedagem>, DespesaHospedagemRepository>();
            service.AddScoped<IDespesasRepository<DespesaAlimentacao>, DespesaAlimentacaoRepository>();
            service.AddScoped<IDespesasRepository<DespesaDeslocamento>, DespesaDeslocamentoRepository>();
            service.AddScoped<IDespesasRepository<DespesaPassagem>, DespesaPassagemRepository>();
            return service;
        }
    }
}
