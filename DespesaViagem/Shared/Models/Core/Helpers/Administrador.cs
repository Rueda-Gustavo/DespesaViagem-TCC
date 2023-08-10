namespace DespesaViagem.Shared.Models.Core.Helpers
{
    public class Administrador : Usuario
    {
        public Administrador()
        {
            TipoDeUsuario = Enums.RolesUsuario.Administrador;
        }
    }
}
