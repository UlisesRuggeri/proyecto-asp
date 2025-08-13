using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EsteroidesToDoDbContext _context;

        public UsuarioRepository(EsteroidesToDoDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExiste(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        public async Task Agregar(Usuario nuevoUsuario)
        {
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
        }
        public async Task AgregarEmpresaId(int usuarioId, int empresaId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return;

            usuario.EmpresaId = empresaId;
            await _context.SaveChangesAsync();
        }

        public async Task<int?> ObtenerEmpresaDelUsuarioAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            return usuario?.EmpresaId;
        }

    }
}
