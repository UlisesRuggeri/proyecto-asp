using EsteroidesToDo.Application.Notifications;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Domain.Interfaces.Notificaciones;

namespace EsteroidesToDo.Application.Services.NotificacionesServices
{
    public class NotificacionesService : INotificacionRepository, INotificacionObserver
    {
        private readonly NotificacionPublisher _publisher;
        private readonly INotificacionRepository _dbNotifier;

        public NotificacionesService(NotificacionPublisher publisher, INotificacionRepository dbNotifier)
        {
            _publisher = publisher;
            _dbNotifier = dbNotifier;

            _publisher.Suscribir(this);
        }

        public async Task NotificacionRecibidaAsync(Notificacion notificacion)
        {
            await _dbNotifier.AgregarAsync(notificacion);
        }

        public async Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int? usuarioId)
        {
            if (usuarioId == null)
                return Enumerable.Empty<Notificacion>();

            return await _dbNotifier.ObtenerPorUsuarioAsync(usuarioId);
        }

        public async Task EncapsularYGuardarNotificacionAsync(int usuarioId, string contenido)
        {
            var notificacion = new Notificacion
            {
                IdUsuario = usuarioId,
                Contenido = contenido
            };
            await AgregarAsync(notificacion);
        }

        public async Task AgregarAsync(Notificacion notificacion)
        {
            await _publisher.Disparar(notificacion);
        }
    }
}
