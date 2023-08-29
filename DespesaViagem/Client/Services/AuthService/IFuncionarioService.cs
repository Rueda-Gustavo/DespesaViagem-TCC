using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.AuthService
{
    public interface IFuncionarioService
    {
        Task<ServiceResponse<Funcionario>> Cadastrar(CadastroUsuario request);
    }
}
