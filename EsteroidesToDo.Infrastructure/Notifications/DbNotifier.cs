

using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Domain.Interfaces.Notificaciones;

namespace EsteroidesToDo.Infrastructure.Notifications
{
    internal class DbNotifier : INotificacionObserver
    {
        private readonly INotificacionRepository _repo;

        public DbNotifier(INotificacionRepository repo)
        {
            _repo = repo;
        }

        //se llama cuando publisher dispara una notificacion
        public async Task NotificacionRecibidaAsync(Notificacion notificacion)
        {
            await _repo.AgregarAsync(notificacion);
        }


        // repositorio: obtiene todas las notificaciones de un usuario
        public async Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _repo.ObtenerPorUsuarioAsync(idUsuario);
        }

    }
}
