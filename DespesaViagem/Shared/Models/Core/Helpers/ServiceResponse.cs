namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class ServiceResponse<T> where T : class
    {
        public T? Conteudo { get; set; }
        public bool Sucesso { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
