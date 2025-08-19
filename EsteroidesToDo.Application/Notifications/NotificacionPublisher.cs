
using EsteroidesToDo.Domain.Interfaces.Notificaciones;

namespace EsteroidesToDo.Application.Notifications
{
    public class NotificacionPublisher
    {
        private readonly List<INotificacionObserver> _observers = new();

        public void Suscribir(INotificacionObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Desuscribir(INotificacionObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public async Task Disparar(Notificacion notificacion)
        {
            foreach(var observer in _observers)
            {
                await observer.NotificacionRecibidaAsync(notificacion);
            }
        }


    }
}
