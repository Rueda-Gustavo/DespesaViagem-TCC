using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Server.Mapping;
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

        public async Task<Result<IEnumerable<FuncionarioDTO>>> ObterTodos()
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterTodos();

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<FuncionarioDTO>>("Não foram encontrados funcionários.");

            IEnumerable<FuncionarioDTO> funcionariosDTO = MappingDTOs.ConverterDTO(funcionarios.ToList());

            return Result.Success(funcionariosDTO);
        }

        public async Task<Result<FuncionarioDTO>> ObterPorId(int id)
        {
            //_ = int.TryParse(id, out int idFuncionario);

            if (id > 0)
            {
                Funcionario funcionario = await _funcionarioRepository.ObterPorId(id);
                if (funcionario is null)
                    return Result.Failure<FuncionarioDTO>("Funcionário não encontrado.");

                FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

                return Result.Success(funcionarioDTO);
            }
            
            return Result.Failure<FuncionarioDTO>("Especifique um id válido!!");
        }

        public async Task<Result<FuncionarioDTO>> ObterPorCPF(string CPF)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorCPF(CPF);

            if (funcionario is null)
                return Result.Failure<FuncionarioDTO>("Funcionário não encontrado. Por favor faça o cadastro dos mesmos.");

            FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            return Result.Success(funcionarioDTO);
            //return Result.Success(funcionario);
        }

        public async Task<Result<IEnumerable<FuncionarioDTO>>> ObterPorFiltro(string filtro)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterPorFiltro(filtro);

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<FuncionarioDTO>>("Esses funcionarios não foram encontrados, por favor faça o cadastro dos mesmos!");

            IEnumerable<FuncionarioDTO> funcionariosDTO = MappingDTOs.ConverterDTO(funcionarios.ToList());

            return Result.Success(funcionariosDTO);
            //return Result.Success(funcionarios);
        }

        public async Task<Result<FuncionarioDTO>> Adicionar(Funcionario funcionario, string password)
        {
            if (await FuncionarioJaExiste(funcionario))
                return Result.Failure<FuncionarioDTO>("Usuário já cadastrado.");

            //funcionario.Gestor = await _gestorRepository.ObterPorId(funcionario.Gestor.Id);

            UsuarioService.CriarPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            funcionario.PasswordHash = passwordHash;
            funcionario.PasswordSalt = passwordSalt;

            await _funcionarioRepository.Insert(funcionario);

            FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            return Result.Success(funcionarioDTO);
            //return Result.Success(funcionario);
        }

        public async Task<Result<FuncionarioDTO>> Alterar(FuncionarioDTO funcionarioDTO)
        {
            if (!await FuncionarioJaExiste(funcionarioDTO))
                return Result.Failure<FuncionarioDTO>("Funcionario não encontrado!");

            Funcionario funcionario = await _funcionarioRepository.ObterPorId(funcionarioDTO.Id);

            funcionario.NomeCompleto = funcionarioDTO.NomeCompleto;
            funcionario.Username = funcionarioDTO.Username;
            funcionario.CPF = funcionarioDTO.CPF;
            funcionario.Matricula = funcionarioDTO.Matricula;

            funcionario.Gestor = (await _funcionarioRepository.ObterPorId(funcionario.Id)).Gestor;

            var duplicidadesFuncionario = await VerificaDuplicidades(funcionario);

            if (duplicidadesFuncionario.IsFailure)
                return Result.Failure<FuncionarioDTO>(duplicidadesFuncionario.Error);

            await _funcionarioRepository.Update(funcionario);

            funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            return Result.Success(funcionarioDTO);
            //return Result.Success(funcionario);
        }

        public async Task<Result<FuncionarioDTO>> Remover(int id)
        {
            Funcionario funcionario = await _funcionarioRepository.ObterPorId(id);
            if (funcionario is null)
                return Result.Failure<FuncionarioDTO>("Funcionario não encontrado!");

            await _funcionarioRepository.Delete(funcionario);
            
            FuncionarioDTO funcionarioDTO = MappingDTOs.ConverterDTO(funcionario);

            return Result.Success(funcionarioDTO);
            //return Result.Success(funcionario);
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
