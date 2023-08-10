using Azure.Core;
using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IGestorRepository _gestorRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, IGestorRepository gestorRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _gestorRepository = gestorRepository;
        }

        public async Task<Result<IEnumerable<Funcionario>>> ObterTodos()
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterTodos();

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<Funcionario>>("Não foram encontrados funcionários.");
            

            return Result.Success(funcionarios);
        }

        public async Task<Result<Funcionario>> ObterPorId(int id)
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

        public async Task<Result<Funcionario>> ObterPorCPF(string CPF)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorCPF(CPF);

            if (funcionario is null)
                return Result.Failure<Funcionario>("Funcionário não encontrado. Por favor faça o cadastro dos mesmos.");

            return Result.Success(funcionario);
        }

        public async Task<Result<IEnumerable<Funcionario>>> ObterPorFiltro(string filtro)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterPorFiltro(filtro);

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<Funcionario>>("Esses funcionarios não foram encontrados, por favor faça o cadastro dos mesmos!");

            return Result.Success(funcionarios);
        }

        public async Task<Result<Funcionario>> Adicionar(Funcionario funcionario)
        {
            if (await FuncionarioJaExiste(funcionario))
                return Result.Failure<Funcionario>("Funcionário já cadastrado.");

            funcionario.Gestor = await _gestorRepository.ObterPorId(funcionario.Gestor.Id);

            funcionario.PasswordHash
                = BCrypt.Net.BCrypt.HashPassword(funcionario.PasswordHash);

            await _funcionarioRepository.Insert(funcionario);
            return Result.Success(funcionario);
        }

        public async Task<Result<Funcionario>> Alterar(Funcionario funcionario)
        {
            if (!await FuncionarioJaExiste(funcionario))
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            funcionario.Gestor = (await _funcionarioRepository.ObterPorId(funcionario.Id)).Gestor;

            await _funcionarioRepository.Update(funcionario);
            return Result.Success(funcionario);
        }              

        public async Task<Result<Funcionario>> Remover(int id)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorId(id);
            if (funcionario is null)
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            await _funcionarioRepository.Delete(funcionario);
            return Result.Success(funcionario);
        }

        private async Task<bool> FuncionarioJaExiste(Funcionario funcionario)
        {          
            if ((await _funcionarioRepository.ObterPorFiltro(funcionario.CPF)).Any() || await _funcionarioRepository.ObterPorId(funcionario.Id) is not null)
            {
                return true;
            }
            return false;
        }
    }
}
