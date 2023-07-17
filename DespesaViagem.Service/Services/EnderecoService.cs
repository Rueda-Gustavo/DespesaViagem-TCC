using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Result<IEnumerable<Endereco>>> ObterTodosEnderecos()
        {
            IEnumerable<Endereco> enderecos = await _enderecoRepository.ObterTodos();
            return Result.FailureIf(enderecos is null, enderecos, "Não foram encontrados endereços.");
        }
        public async Task<Result<Endereco>> ObterEnderecoPorId(int idEndereco)
        {
            //_ = int.TryParse(id, out int idEndereco);

            if (idEndereco > 0)
            {
                Endereco endereco = await _enderecoRepository.ObterPorId(idEndereco);

                if (endereco is null)
                    return Result.Failure<Endereco>("Endereço não encontrado.");

                return Result.Success(endereco);
            }

            return Result.Failure<Endereco>("Especifique um id válido!!");
        }        

        public async Task<Result<IEnumerable<Endereco>>> ObterEnderecoPorFiltro(string filtro)
        {
            IEnumerable<Endereco> enderecos = await _enderecoRepository.ObterPorFiltro(filtro);

            if (!enderecos.Any())
                return Result.Failure<IEnumerable<Endereco>>("Esses endereços não foram encontrados!");

            return Result.Success(enderecos);
        }

        public async Task<Result<Endereco>> ObterEnderecoPorFiltro(Endereco endereco)
        {
            IEnumerable<Endereco> enderecos = await _enderecoRepository.ObterPorFiltro(endereco.Logradouro);

            Endereco? verificacao = enderecos.FirstOrDefault(enderecoTemp =>
            (enderecoTemp.CEP == endereco.CEP) &&
            (enderecoTemp.NumeroCasa == endereco.NumeroCasa) &&
            (enderecoTemp.Cidade == endereco.Cidade) &&
            (enderecoTemp.Estado == endereco.Estado));

            if (!enderecos.Any() && verificacao is not null)
                return Result.Failure<Endereco>("Esse endereço não foi encontrado!");

            return Result.Success(enderecos.First());
        }

        public async Task<Result<Endereco>> AdicionarEndereco(Endereco endereco)
        {
            if (await EnderecoJaExiste(endereco))
                return Result.Failure<Endereco>("Endereço já cadastrado.");
            
            await _enderecoRepository.Insert(endereco);
            return Result.Success(endereco);
        }
        public async Task<Result<Endereco>> AlterarEndereco(Endereco endereco)
        {
            if (!await EnderecoJaExiste(endereco))
                return Result.Failure<Endereco>("Endereço não encontrado!");

            await _enderecoRepository.Update(endereco);
            return Result.Success(endereco);
        }

        public async Task<Result<Endereco>> RemoverEndereco(int id)
        {
            Endereco endereco = await _enderecoRepository.ObterPorId(id);
            if (endereco is null)
                return Result.Failure<Endereco>("Endereço não encontrado!");

            await _enderecoRepository.Delete(endereco);
            return Result.Success(endereco);
        }

        private async Task<bool> EnderecoJaExiste(Endereco endereco)
        {
            endereco = await _enderecoRepository.ObterPorId(endereco.Id);

            if (endereco is not null)
                return true;

            IEnumerable<Endereco> enderecos = await _enderecoRepository.ObterPorFiltro(endereco.Logradouro);                            

            if (!enderecos.Any())
                enderecos = await _enderecoRepository.ObterPorFiltro(endereco.CEP);

            return enderecos.Any(enderecoTemp =>
            (enderecoTemp.CEP == endereco.CEP) &&
            (enderecoTemp.NumeroCasa == endereco.NumeroCasa) &&
            (enderecoTemp.Cidade == endereco.Cidade) &&
            (enderecoTemp.Estado == endereco.Estado));            
        }        
    }
}
