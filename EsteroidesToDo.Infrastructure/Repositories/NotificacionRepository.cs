

using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Infrastructure.Repositories
{
    internal class NotificacionRepository : INotificacionRepository
    {
        private readonly EsteroidesToDoDbContext _context;

        public NotificacionRepository(EsteroidesToDoDbContext context)
        {
            _context = context;
        }
        
        public async Task AgregarAsync(Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int? idUsuario)
        {
            return await _context.Notificaciones
                                 .Where(u => u.IdUsuario == idUsuario)
                                 .OrderByDescending(n => n.FechaCreacion)
                                 .ToListAsync();
        }
    }
}
