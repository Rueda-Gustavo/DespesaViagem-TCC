using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.DTOs.Helpers;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Core.Helpers;

namespace DespesaViagem.Services.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository, IUsuarioRepository usuarioRepository)
        {
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<IEnumerable<Departamento>>> ObterDepartamentos(int idAdmin)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(idAdmin);

            if (usuario is null || usuario.TipoDeUsuario != RolesUsuario.Administrador)
                return Result.Failure<IEnumerable<Departamento>>("Usuário não encontrado ou não está autorizado!");

            IEnumerable<Departamento> departamentos = await _departamentoRepository.ObterDepartamentos();

            if (!departamentos.Any())
                return Result.Failure<IEnumerable<Departamento>>("Nenhum departamento foi encontrado.");

            return Result.Success(departamentos);
        }
        public async Task<Result<Departamento>> ObterDepartamento(int id)
        {
            Departamento departamento = await _departamentoRepository.ObterDepartamento(id);

            if (departamento is null)
                return Result.Failure<Departamento>("Nenhum departamento foi encontrado.");

            return Result.Success(departamento);
        }

        public async Task<Result<Departamento>> ObterDepartamento(string descricao)
        {
            Departamento departamento = await _departamentoRepository.ObterDepartamento(descricao);

            if (departamento is null)
                return Result.Failure<Departamento>("Nenhum departamento foi encontrado.");

            return Result.Success(departamento);
        }
        public async Task<Result<Departamento>> AdicionarDepartamento(string descricao)
        {
            Departamento departamento = await _departamentoRepository.ObterDepartamento(descricao);
            if (departamento is not null)
                return Result.Failure<Departamento>("Já existe um departamento com essa descrição.");

            departamento = new Departamento { Id = 0, Descricao = descricao };

            await _departamentoRepository.Insert(departamento);
            return Result.Success(departamento);
        }

        public async Task<Result<Departamento>> AlterarDepartamento(Departamento departamento)
        {
            Departamento departamentoExistente = await _departamentoRepository.ObterDepartamento(departamento.Descricao);
            if (departamentoExistente is null)
                return Result.Failure<Departamento>("Departamento não encontrado.");

            departamento.Id = departamentoExistente.Id;
            
            await _departamentoRepository.Update(departamento);
            return Result.Success(departamento);
        }

        public async Task<Result<Departamento>> DesativarDepartamento(int id)
        {
            Departamento departamentoExistente = await _departamentoRepository.ObterDepartamento(id);
            if (departamentoExistente is null)
                return Result.Failure<Departamento>("Departamento não encontrado.");

            departamentoExistente.Ativo = false;

            await _departamentoRepository.Update(departamentoExistente);
            return Result.Success(departamentoExistente);
        }

        public async Task<Result<Departamento>> AtivarDepartamento(int id)
        {
            Departamento departamentoExistente = await _departamentoRepository.ObterDepartamento(id);
            if (departamentoExistente is null)
                return Result.Failure<Departamento>("Departamento não encontrado.");

            departamentoExistente.Ativo = true;

            await _departamentoRepository.Update(departamentoExistente);
            return Result.Success(departamentoExistente);
        }

        public async Task<Result<Departamento>> RemoverDepartamento(int id)
        {
            Departamento departamentoExistente = await _departamentoRepository.ObterDepartamento(id);
            if (departamentoExistente is null)
                return Result.Failure<Departamento>("Departamento não encontrado.");

            await _departamentoRepository.Delete(departamentoExistente);
            return Result.Success(departamentoExistente);
        }
    }
}
