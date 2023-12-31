﻿namespace DespesaViagem.Infra.Interfaces
{
    public interface IDespesasRepository<T> where T : class
    {
        //Os métodos get irão retornar todas as despesas relacionadas apenas ao seu tipo T definido no corpo do método
        //Por exemplo: DespesaHospedagemRepository, irá retornar todas as depesas do tipo Hospedagem relacionadas a viagem especificada
        Task<IEnumerable<T>> ObterTodos(List<int> idsViagens);
        Task<IEnumerable<T>> ObterTodos(int idViagem);
        Task<T> ObterPorId(int id);
        Task<IEnumerable<T>> ObterPorFiltro(string filtro, int idViagem);
        Task Insert(T despesa);
        Task Update(T despesa);
        Task Delete(T despesa);
    }
}
