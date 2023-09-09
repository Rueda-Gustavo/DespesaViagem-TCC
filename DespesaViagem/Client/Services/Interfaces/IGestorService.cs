using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IGestorService
    {
        Task<List<Funcionario>> ObterListaDeFuncionarios();
    }
}
