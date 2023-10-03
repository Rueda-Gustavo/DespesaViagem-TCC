namespace DespesaViagem.Shared.DTOs.Helpers
{
    public class GestorDTO : UsuarioDTO
    {
        public List<FuncionarioDTO> Funcionarios { get; set; } = new();
    }
}
