using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Result<IEnumerable<Funcionario>>> ObterTodosFuncionarios()
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterTodos();

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<Funcionario>>("Não foram encontrados funcionários.");
            

            return Result.Success(funcionarios);
        }

        public async Task<Result<Funcionario>> ObterFuncionarioPorId(int id)
        {
            //_ = int.TryParse(id, out int idFuncionario);

            if(id > 0)
            {
                Funcionario funcionario = await _funcionarioRepository.ObterPorId(id);
                if (funcionario is null)
                    return Result.Failure<Funcionario>("Funcionário não encontrado.");

                return Result.Success(funcionario); 
            }

            return Result.Failure<Funcionario>("Especifique um id válido!!");            
        }

        public async Task<Result<Funcionario>> ObterFuncionarioPorCPF(string CPF)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorCPF(CPF);

            if (funcionario is null)
                return Result.Failure<Funcionario>("Funcionário não encontrado. Por favor faça o cadastro dos mesmos.");

            return Result.Success(funcionario);
        }

        public async Task<Result<IEnumerable<Funcionario>>> ObterFuncionarioPorFiltro(string filtro)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterPorFiltro(filtro);

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<Funcionario>>("Esses funcionarios não foram encontrados, por favor faça o cadastro dos mesmos!");

            return Result.Success(funcionarios);
        }

        public async Task<Result<Funcionario>> AdicionarFuncionario(Funcionario funcionario)
        {
            if (await FuncionarioJaExiste(funcionario))
                return Result.Failure<Funcionario>("Funcionário já cadastrado.");
            
            await _funcionarioRepository.Insert(funcionario);
            return Result.Success(funcionario);
        }

        public async Task<Result<Funcionario>> AlterarFuncionario(Funcionario funcionario)
        {
            if (!await FuncionarioJaExiste(funcionario))
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            await _funcionarioRepository.Update(funcionario);
            return Result.Success(funcionario);
        }              

        public async Task<Result<Funcionario>> RemoverFuncionario(int id)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorId(id);
            if (funcionario is null)
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            await _funcionarioRepository.Delete(funcionario);
            return Result.Success(funcionario);
        }

        private async Task<bool> FuncionarioJaExiste(Funcionario funcionario)
        {          
            if ((await _funcionarioRepository.ObterPorFiltro(funcionario.CPF)).Any())
            {
                return true;
            }
            return false;
        }
    }
}
