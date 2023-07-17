﻿using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IViagemService _viagemService;
        private readonly IDespesaRepository _despesaRepository;

        public DespesaService(IDespesaRepository despesaRepository, IViagemService viagemService)
        {
            _despesaRepository = despesaRepository;
            _viagemService = viagemService;
        }

        public async Task<Result<IEnumerable<Despesa>>> ObterTodasDespesas(int idViagem)
        {
            Task<Result<Viagem>> viagem = _viagemService.ObterViagemPorId(idViagem);
            viagem.Wait();
            idViagem = viagem.Result.Value.Id;
            IEnumerable<Despesa> despesa = await _despesaRepository.ObterTodos(idViagem);
            return Result.FailureIf(despesa is null, despesa, "Não existem despesas para a viagem informada!!");
        }

        public async Task<Result<Despesa>> ObterDespesaPorId(string id)
        {
            _ = int.TryParse(id, out int idDespesa);

            if (idDespesa > 0)
            {
                Despesa despesa = await _despesaRepository.ObterPorId(idDespesa);
                return Result.FailureIf(despesa is null, despesa, "Essa despesa não foi encontrada ou não existe!");
            }

            return Result.Failure<Despesa>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<Despesa>>> ObterDespesasPorFiltro(string filtro, int idViagem)
        {
            IEnumerable<Despesa> despesas = await _despesaRepository.ObterPorFiltro(filtro, idViagem);
            return Result.FailureIf(despesas is null, despesas, "Essas despesas não foram encontradas ou não existem!");
        }      
    }
}
