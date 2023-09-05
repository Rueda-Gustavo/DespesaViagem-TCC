using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Client.Services.Interfaces
{
    public interface IEnderecoService
    {
        string Mensagem { get; set; }
        Endereco Endereco { get; set; }
        Task GetEnderecos();
        Task GetEndereco(int idEndereco);

    }
}
