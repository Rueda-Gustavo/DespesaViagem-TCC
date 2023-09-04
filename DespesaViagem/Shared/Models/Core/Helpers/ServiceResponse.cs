namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class ServiceResponse<T>
    {
        public T? Conteudo { get; set; }
        public bool Sucesso { get; set; } = true;
        public string Mensagem { get; set; } = string.Empty;
    }
}
