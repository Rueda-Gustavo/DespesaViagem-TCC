using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IGestorRepository _gestorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                                  IGestorRepository gestorRepository,
                                  IUsuarioRepository usuarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _gestorRepository = gestorRepository;
            _usuarioRepository = usuarioRepository;
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

            if (id > 0)
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

        public async Task<Result<Funcionario>> Adicionar(Funcionario funcionario, string password)
        {

            if (await FuncionarioJaExiste(funcionario))
                return Result.Failure<Funcionario>("Funcionário já cadastrado.");

            //funcionario.Gestor = await _gestorRepository.ObterPorId(funcionario.Gestor.Id);

            UsuarioService.CriarPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            funcionario.PasswordHash = passwordHash;
            funcionario.PasswordSalt = passwordSalt;

            await _funcionarioRepository.Insert(funcionario);
            return Result.Success(funcionario);
        }

        public async Task<Result<Funcionario>> Alterar(FuncionarioDTO funcionarioDTO)
        {
            if (!await FuncionarioJaExiste(funcionarioDTO))
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            Funcionario funcionario = await _funcionarioRepository.ObterPorId(funcionarioDTO.Id);

            funcionario.NomeCompleto = funcionarioDTO.NomeCompleto;
            funcionario.Username = funcionarioDTO.Username;
            funcionario.CPF = funcionarioDTO.CPF;
            funcionario.Matricula = funcionarioDTO.Matricula;

            funcionario.Gestor = (await _funcionarioRepository.ObterPorId(funcionario.Id)).Gestor;

            var duplicidadesFuncionario = await VerificaDuplicidades(funcionario);

            if (duplicidadesFuncionario.IsFailure)
                return Result.Failure<Funcionario>(duplicidadesFuncionario.Error);

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

            if (await _usuarioRepository.UsuarioJaExiste(funcionario.CPF) ||
                await _usuarioRepository.UsuarioJaExiste(funcionario.Username) ||
                (funcionario.Matricula is not null && await _funcionarioRepository.FuncionarioJaExiste(funcionario.Matricula)))
            {
                return true;
            }

            return false;
        }

        private async Task<bool> FuncionarioJaExiste(FuncionarioDTO funcionario)
        {

            if (await _usuarioRepository.UsuarioJaExiste(funcionario.CPF) ||
                await _usuarioRepository.UsuarioJaExiste(funcionario.Username) ||
                (funcionario.Matricula is not null && await _funcionarioRepository.FuncionarioJaExiste(funcionario.Matricula)))
            {
                return true;
            }

            return false;
        }

        private async Task<Result<string>> VerificaDuplicidades(Funcionario funcionario)
        {
            if ((await _funcionarioRepository.ObterPorFiltro(funcionario.Username)).Count() > 1)
            {
                return Result.Failure<string>("Username já está em uso!");
            }

            var matricula = await _funcionarioRepository.ObterPorFiltro(funcionario.Matricula);

            if (matricula is not null && matricula.Count() > 1)
            {
                return Result.Failure<string>("Matrícula já cadastrada!");
            }

            return Result.Success("Sem duplicidades");
        }
    }
}
