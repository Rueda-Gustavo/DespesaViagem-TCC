using DespesaViagem.Client;
using DespesaViagem.Client.Services.DespesasService;
using DespesaViagem.Client.Services.EnderecoService;
using DespesaViagem.Client.Services.Interfaces;
using DespesaViagem.Client.Services.ViagemService;
using DespesaViagem.Shared.DTOs.Despesas;
using DespesaViagem.Shared.Models.Despesas;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IViagemService, ViagemService>();
builder.Services.AddScoped<IDespesasService<DespesaHospedagemDTO>, DespesaHospedagemService>();
builder.Services.AddScoped<IDespesasService<DespesaAlimentacaoDTO>, DespesaAlimentacaoService>();
builder.Services.AddScoped<IDespesasService<DespesaDeslocamentoDTO>, DespesaDeslocamentoService>();
builder.Services.AddScoped<IDespesasService<DespesaPassagemDTO>, DespesaPassagemService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

await builder.Build().RunAsync();
