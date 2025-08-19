
namespace EsteroidesToDo.Domain.Interfaces
{
    public interface INotificacionRepository
    {
        Task AgregarAsync(Notificacion notificacion);
        Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int? idUsuario);
    }

}
