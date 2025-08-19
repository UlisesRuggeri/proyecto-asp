
namespace EsteroidesToDo.Domain.Interfaces.Notificaciones
{
    public interface INotificacionObserver
    {
        Task NotificacionRecibidaAsync(Notificacion notificacion);
    }
}
