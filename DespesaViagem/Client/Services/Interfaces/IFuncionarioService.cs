using CSharpFunctionalExtensions;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;
using static System.Net.WebRequestMethods;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IFuncionarioService
    {
        event Action FuncionariosChanged;
        string Mensagem { get; set; }
        Task<ServiceResponse<int>> Cadastrar(CadastroUsuario request);
        Task<FuncionarioDTO> GetFuncionario(int idFuncionario);
        Task<FuncionarioDTO> GetFuncionario(string CPF);
        Task<Result<FuncionarioDTO>> AtualizarPerfil(FuncionarioDTO funcionario);
    }
}
