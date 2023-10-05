using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IAdminService
    {
        event Action GestoresChanged;
        string Mensagem { get; set; }
        Task<List<FuncionarioDTO>> ObterListaDeFuncionarios();
    }
}
