namespace DespesaViagem.Shared.DTOs.Helpers
{
    public class FuncionarioDTO : UsuarioDTO
    {        
        public string? Matricula { get; set; } = string.Empty;
        public int GestorId { get; set; }
        public string? GestorUsername { get; set; } = string.Empty;
    }
}
