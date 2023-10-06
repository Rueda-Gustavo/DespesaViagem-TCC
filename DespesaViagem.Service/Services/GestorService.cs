using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Server.Mapping;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class GestorService : IGestorService
    {
        private readonly IGestorRepository _gestorRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public GestorService(IGestorRepository gestorRepository, IFuncionarioRepository funcionarioRepository)
        {
            _gestorRepository = gestorRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Result<IEnumerable<GestorDTO>>> ObterTodos()
        {
            IEnumerable<Gestor> gestores = await _gestorRepository.ObterTodos();

            if (!gestores.Any())
                return Result.Failure<IEnumerable<GestorDTO>>("Não foram encontrados Gestores.");

            IEnumerable<GestorDTO> gestoresDTO = MappingDTOs.ConverterDTO(gestores.ToList());

            return Result.Success(gestoresDTO);
        }

        public async Task<Result<IEnumerable<FuncionarioDTO>>> ObterListaFuncionarios(int gestorId)
        {
            IEnumerable<Funcionario> funcionarios = await _funcionarioRepository.ObterFuncionariosPorGestor(gestorId);

            if (!funcionarios.Any())
                return Result.Failure<IEnumerable<FuncionarioDTO>>("Não foram encontrados funcionarios.");

            IEnumerable<FuncionarioDTO> funcionariosDTO = MappingDTOs.ConverterDTO(funcionarios.ToList());

            return Result.Success(funcionariosDTO);
        }

        public async Task<Result<GestorDTO>> ObterPorId(int id)
        {
            //_ = int.TryParse(id, out int idFuncionario);

            if (id > 0)
            {
                Gestor gestor = await _gestorRepository.ObterPorId(id);
                if (gestor is null)
                    return Result.Failure<GestorDTO>("Gestor não encontrado.");

                GestorDTO gestorDTO = MappingDTOs.ConverterDTO(gestor);

                return Result.Success(gestorDTO);
            }

            return Result.Failure<GestorDTO>("Especifique um id válido!!");
        }

        public async Task<Result<GestorDTO>> ObterPorCPF(string CPF)
        {
            Gestor gestor = await _gestorRepository.ObterPorCPF(CPF);

            if (gestor is null)
                return Result.Failure<GestorDTO>("Gestor não encontrado. Por favor faça o cadastro do mesmo.");

            GestorDTO gestorDTO = MappingDTOs.ConverterDTO(gestor);

            return Result.Success(gestorDTO);
        }

        public async Task<Result<IEnumerable<GestorDTO>>> ObterPorFiltro(string filtro)
        {
            IEnumerable<Gestor> gestores = await _gestorRepository.ObterPorFiltro(filtro);

            if (!gestores.Any())
                return Result.Failure<IEnumerable<GestorDTO>>("Esses gestores não foram encontrados, por favor faça o cadastro dos mesmos!");

            IEnumerable<GestorDTO> gestoresDTO = MappingDTOs.ConverterDTO(gestores.ToList());

            return Result.Success(gestoresDTO);
        }

        public async Task<Result<GestorDTO>> Adicionar(Gestor gestor, string password)
        {
            if (await GestorJaExiste(gestor))
                return Result.Failure<GestorDTO>("Usuário já cadastrado.");

            UsuarioService.CriarPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            gestor.PasswordHash = passwordHash;
            gestor.PasswordSalt = passwordSalt;

            await _gestorRepository.Insert(gestor);

            GestorDTO gestorDTO = MappingDTOs.ConverterDTO(gestor);

            return Result.Success(gestorDTO);
        }

        /*
                     if (!await FuncionarioJaExiste(funcionarioDTO))
                return Result.Failure<Funcionario>("Funcionario não encontrado!");

            Funcionario funcionario = await _funcionarioRepository.ObterPorId(funcionarioDTO.Id);

            funcionario.NomeCompleto = funcionarioDTO.NomeCompleto;
            funcionario.Username = funcionarioDTO.Username;
            funcionario.CPF = funcionarioDTO.CPF;
            funcionario.Matricula = funcionarioDTO.Matricula;
         */

        public async Task<Result<GestorDTO>> Alterar(GestorDTO gestorDTO)
        {
            if (!await GestorJaExiste(gestorDTO))
                return Result.Failure<GestorDTO>("Gestor não encontrado!");

            Gestor gestor = await _gestorRepository.ObterPorId(gestorDTO.Id);

            gestor.NomeCompleto = gestorDTO.NomeCompleto;
            gestor.Username = gestorDTO.Username;
            gestor.CPF = gestorDTO.CPF;

            var duplicidadesGestor = await VerificaDuplicidades(gestor);

            if (duplicidadesGestor.IsFailure)
                return Result.Failure<GestorDTO>(duplicidadesGestor.Error);

            await _gestorRepository.Update(gestor);
            return Result.Success(gestorDTO);
        }

        public async Task<Result<GestorDTO>> Remover(int id)
        {
            Gestor gestor = await _gestorRepository.ObterPorId(id);
            if (gestor is null)
                return Result.Failure<GestorDTO>("Gestor não encontrado!");

            await _gestorRepository.Delete(gestor);

            GestorDTO gestorDTO = MappingDTOs.ConverterDTO(gestor);
            return Result.Success(gestorDTO);
        }
        /*
        private async Task<bool> GestorJaExiste(Gestor gestor)
        {
            if ((await _gestorRepository.ObterPorFiltro(gestor.CPF)).Any() || await _gestorRepository.ObterPorId(gestor.Id) is not null)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> GestorJaExiste(GestorDTO gestor)
        {
            if ((await _gestorRepository.ObterPorFiltro(gestor.CPF)).Any() || await _gestorRepository.ObterPorId(gestor.Id) is not null)
            {
                return true;
            }
            return false;
        }
        */
        private async Task<bool> GestorJaExiste(Gestor gestor)
        {

            if (await _gestorRepository.UsuarioJaExiste(gestor.CPF) ||
                await _gestorRepository.UsuarioJaExiste(gestor.Username))
            {
                return true;
            }

            return false;
        }

        private async Task<bool> GestorJaExiste(GestorDTO gestor)
        {

            if (await _gestorRepository.UsuarioJaExiste(gestor.CPF) ||
                await _gestorRepository.UsuarioJaExiste(gestor.Username))
            {
                return true;
            }

            return false;
        }

        private async Task<Result<string>> VerificaDuplicidades(Gestor gestor)
        {
            if ((await _gestorRepository.ObterPorFiltro(gestor.Username)).Count() > 1)
            {
                return Result.Failure<string>("Username já está em uso!");
            }            

            return Result.Success("Sem duplicidades");
        }
    }
}
