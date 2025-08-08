
namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<bool> EmailExiste(string email);
        Task Agregar(Usuario nuevoUsuario);
        Task AgregarEmpresaId(int usuarioId, int empresaId);
        Task<int?> UsuarioYaTieneEmpresa(int usuarioId);
    }
}
