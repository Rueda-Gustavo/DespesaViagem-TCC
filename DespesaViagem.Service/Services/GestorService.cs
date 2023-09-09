using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class GestorService : IGestorService
    {
        private readonly IGestorRepository _gestorRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public GestorService(IGestorRepository GestorRepository, IFuncionarioRepository funcionarioRepository)
        {
            _gestorRepository = GestorRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Result<IEnumerable<Gestor>>> ObterTodos()
        {
            IEnumerable<Gestor> gestores = await _gestorRepository.ObterTodos();

            if (!gestores.Any())
                return Result.Failure<IEnumerable<Gestor>>("Não foram encontrados Gestores.");


            return Result.Success(gestores);
        }

        public async Task<Result<IEnumerable<Funcionario>>> ObterListaFuncionarios(int gestorId)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterFuncionariosPorGestor(gestorId);

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<Funcionario>>("Não foram encontrados funcionarios.");

            return Result.Success(funcionarios);
        }

        public async Task<Result<Gestor>> ObterPorId(int id)
        {
            //_ = int.TryParse(id, out int idFuncionario);

            if (id > 0)
            {
                Gestor Gestor = await _gestorRepository.ObterPorId(id);
                if (Gestor is null)
                    return Result.Failure<Gestor>("Gestor não encontrado.");

                return Result.Success(Gestor);
            }

            return Result.Failure<Gestor>("Especifique um id válido!!");
        }

        public async Task<Result<Gestor>> ObterPorCPF(string CPF)
        {
            Gestor Gestor = await _gestorRepository.ObterPorCPF(CPF);

            if (Gestor is null)
                return Result.Failure<Gestor>("Gestor não encontrado. Por favor faça o cadastro do mesmo.");

            return Result.Success(Gestor);
        }

        public async Task<Result<IEnumerable<Gestor>>> ObterPorFiltro(string filtro)
        {
            IEnumerable<Gestor> gestores = await _gestorRepository.ObterPorFiltro(filtro);

            if (!gestores.Any())
                return Result.Failure<IEnumerable<Gestor>>("Esses gestores não foram encontrados, por favor faça o cadastro dos mesmos!");

            return Result.Success(gestores);
        }

        public async Task<Result<Gestor>> Adicionar(Gestor gestor)
        {
            if (await GestorJaExiste(gestor))
                return Result.Failure<Gestor>("Gestor já cadastrado.");

            await _gestorRepository.Insert(gestor);
            return Result.Success(gestor);
        }

        public async Task<Result<Gestor>> Alterar(Gestor gestor)
        {
            if (!await GestorJaExiste(gestor))
                return Result.Failure<Gestor>("Gestor não encontrado!");

            await _gestorRepository.Update(gestor);
            return Result.Success(gestor);
        }

        public async Task<Result<Gestor>> Remover(int id)
        {
            Gestor Gestor = await _gestorRepository.ObterPorId(id);
            if (Gestor is null)
                return Result.Failure<Gestor>("Gestor não encontrado!");

            await _gestorRepository.Delete(Gestor);
            return Result.Success(Gestor);
        }

        private async Task<bool> GestorJaExiste(Gestor gestor)
        {
            if ((await _gestorRepository.ObterPorFiltro(gestor.CPF)).Any() || await _gestorRepository.ObterPorId(gestor.Id) is not null)
            {
                return true;
            }
            return false;
        }
    }
}
