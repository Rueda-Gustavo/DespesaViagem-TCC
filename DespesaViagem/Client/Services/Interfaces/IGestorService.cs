using CSharpFunctionalExtensions;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IGestorService
    {
        event Action GestoresChanged;
        string Mensagem { get; set; }
        Task<List<Funcionario>> ObterListaDeFuncionarios();
        Task<GestorDTO> GetGestor(int idGestor);
        Task<GestorDTO> GetGestor(string CPF);
        Task<Result<GestorDTO>> AtualizarPerfil(GestorDTO gestor);
    }
}
