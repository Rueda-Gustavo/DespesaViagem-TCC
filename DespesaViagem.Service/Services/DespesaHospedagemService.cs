﻿using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Infra.Repositories;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Enums;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class DespesaHospedagemService : IDespesasService<DespesaHospedagem>
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IDespesasRepository<DespesaHospedagem> _despesaRepository;
        //private readonly IEnderecoRepository _enderecoRepository;

        public DespesaHospedagemService(IDespesasRepository<DespesaHospedagem> despesaRepository,
            IViagemRepository viagemRepository/*, IEnderecoRepository enderecoRepository*/)
        {
            _viagemRepository = viagemRepository;
            _despesaRepository = despesaRepository;
            //_enderecoRepository = enderecoRepository;
        }

        public async Task<Result<IEnumerable<DespesaHospedagem>>> ObterTodasDespesas(int idViagem)
        {            
            Viagem viagem = await _viagemRepository.ObterPorId(idViagem);

            if (viagem is null)
                return Result.Failure<IEnumerable<DespesaHospedagem>>("Informe uma viagem válida.");
                        
            IEnumerable<DespesaHospedagem> despesa = await _despesaRepository.ObterTodos(idViagem);
            return Result.FailureIf(despesa is null, despesa, "Não existem despesas para a viagem informada!!");
        }        

        public async Task<Result<DespesaHospedagem>> ObterDespesaPorId(string id)
        {
            _ = int.TryParse(id, out int idDespesa);

            if (idDespesa > 0)
            {
                DespesaHospedagem despesa = await _despesaRepository.ObterPorId(idDespesa);
                return Result.FailureIf(despesa is null, despesa, "Essa despesa não foi encontrada ou não existe!");
            }

            return Result.Failure<DespesaHospedagem>("Especifique um id válido!!");
        }

        public async Task<Result<IEnumerable<DespesaHospedagem>>> ObterDespesasPorFiltro(string filtro, string idViagem)
        {
            _ = int.TryParse(idViagem, out int id);

            if (id > 0)
            {
                IEnumerable<DespesaHospedagem> despesas = await _despesaRepository.ObterPorFiltro(filtro, id);
                return Result.FailureIf(despesas is null, despesas, "Essas despesas não foram encontradas ou não existem!");
            }

            return Result.Failure<IEnumerable<DespesaHospedagem>>("Especifique um id válido para a viagem.");
        }

        public async Task<Result<DespesaHospedagem>> AdicionarDespesa(DespesaHospedagem despesa)
        {
            if (await _despesaRepository.ObterPorId(despesa.Id) is not null)
                return Result.Failure<DespesaHospedagem>("Já existe uma despesa como essa!");

            /*
            if (await _enderecoRepository.ObterPorId(despesa.IdEndereco) is null || despesa.IdEndereco <= 0)
                return Result.Failure<DespesaHospedagem>("Forneça um endereço já existente, ou cadastre um novo.");
            */

            Viagem viagem = await _viagemRepository.ObterPorId(despesa.IdViagem);

            if (viagem is null || (viagem.StatusViagem != StatusViagem.Aberta && viagem.StatusViagem != StatusViagem.EmAndamento))
                return Result.Failure<DespesaHospedagem>("Viagem não encontrada ou não existe uma viagem aberta ou em andamento.");

            if (despesa.ValorDiaria <= 0 || despesa.TotalDespesa <= 0)
                return Result.Failure<DespesaHospedagem>("Insira um valor válido para a despesa.");

            if (despesa.NomeDespesa.Length < 4 || despesa.DescricaoDespesa.Length < 4)
                return Result.Failure<DespesaHospedagem>("Preencha os campos Nome e Descrição para a despesa. (Mínimo de 4 caracteres)");

            viagem.AdicionarDespesa(despesa);
            viagem.AtualizarTotalDespesas();

            //await _viagemRepository.Update(viagem);         
            await _despesaRepository.Insert(despesa);
            return Result.Success(despesa);
        }

        public async Task<Result<DespesaHospedagem>> AlterarDespesa(DespesaHospedagem despesa)
        {
            DespesaHospedagem despesaAtual = await _despesaRepository.ObterPorId(despesa.Id);

            if (despesaAtual is null)
                return Result.Failure<DespesaHospedagem>("Despesa não encontrada!");

            if (despesaAtual.TotalDespesa != despesa.TotalDespesa && despesa.TotalDespesa > 0)
            {
                Viagem viagem = await _viagemRepository.ObterPorId(despesa.IdViagem);
                viagem.AtualizarDespesa(despesa);
                await _viagemRepository.Update(viagem);
            }

            if (despesa.NomeDespesa.Length < 4 || despesa.DescricaoDespesa.Length < 4)
                return Result.Failure<DespesaHospedagem>("Preencha os campos Nome e Descrição para a despesa. (Mínimo de 4 caracteres)");

            await _despesaRepository.Update(despesa);
            return Result.Success(despesa);
        }

        public async Task<Result<DespesaHospedagem>> RemoverViagem(int id)
        {
            DespesaHospedagem despesa = await _despesaRepository.ObterPorId(id);
            if (despesa is null)
                return Result.Failure<DespesaHospedagem>("Despesa não encontrada!");

            await _despesaRepository.Delete(despesa);
            return Result.Success(despesa); 
        }
    }
}
