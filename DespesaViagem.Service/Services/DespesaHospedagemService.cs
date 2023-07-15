using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Despesas;
using DespesaViagem.Shared.Models.Viagens;

namespace DespesaViagem.Services.Services
{
    public class DespesaHospedagemService : IDespesasService<DespesaHospedagem, int>
    {
        private readonly IViagemService _viagemService;
        private readonly IDespesasRepository<DespesaHospedagem, int> _despesaRepository;
        public DespesaHospedagemService(IDespesasRepository<DespesaHospedagem, int> despesaRepository, IViagemService viagemService)
        {
            _viagemService = viagemService;
            _despesaRepository = despesaRepository;
        }
        
        public async Task<Result<IEnumerable<DespesaHospedagem>>> ObterTodasDespesas(int idViagem)
        {            
            Task<Result<Viagem>> viagem = _viagemService.ObterViagemPorId(idViagem.ToString());
            viagem.Wait();
            idViagem = viagem.Result.Value.Id;
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

        public async Task<Result<DespesaHospedagem>> AdicionarDespesa(DespesaHospedagem despesa, Viagem viagem)
        {
            if (await DespesaJaExiste(despesa.Id))
                return Result.Failure<DespesaHospedagem>("Já existe uma despesa como essa!");
            
            await _despesaRepository.Insert(despesa, viagem);
            return Result.Success(despesa);
        }

        public async Task<Result<DespesaHospedagem>> AlterarDespesa(DespesaHospedagem despesa)
        {
            if (!await DespesaJaExiste(despesa.Id))
                return Result.Failure<DespesaHospedagem>("Despesa não encontrada!");

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

        private async Task<bool> DespesaJaExiste(int id)
        {
            if(await _despesaRepository.ObterPorId(id) is not null)
            {
                return true;
            }
            return false;
        }
    }
}
