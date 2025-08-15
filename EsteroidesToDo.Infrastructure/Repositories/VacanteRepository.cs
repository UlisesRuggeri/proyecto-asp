using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Infrastructure.Data;
using EsteroidesToDo.Models;
using Microsoft.EntityFrameworkCore;


namespace EseroidesToDo.Infrastructure.Repositories
{
    internal class VacanteRepository : IVacanteRepository
    {
        private readonly EsteroidesToDoDbContext _context;
        public VacanteRepository(EsteroidesToDoDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PuedeCrearVacanteAsync(int? usuarioId)
        {
            return await _context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.IdDuenio == usuarioId);
        }

        public async Task AgregarVacanteAsync(Vacante vacante)
        {
            _context.Vacantes.Add(vacante);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vacante>> ObtenerTodasAsync()
        {
            return await _context.Vacantes
                .Include(v => v.Empresa)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Vacante>> ObtenerPorEmpresaAsync(int usuarioId)
        {
            var empresaId = await _context.Usuarios
                .Where(u => u.Id == usuarioId)
                .Select(u => u.EmpresaId)
                .FirstOrDefaultAsync();

            return await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .Where(v => v.UsuarioVacantes.Any(uv => uv.UsuarioId == usuarioId) || v.EmpresaId == empresaId)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task MarcarPostuladoComoAceptadoAsync(int vacanteId, int usuarioId)
        {
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            var postulado = vacante.UsuarioVacantes
                .FirstOrDefault(p => p.UsuarioId == usuarioId);

            _context.UsuarioVacantes.Remove(postulado);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            usuario.EmpresaId = vacante.EmpresaId;

            await _context.SaveChangesAsync();
        }


        public async Task MarcarPostuladoComoRechazadoAsync(int vacanteId, int usuarioId)
        {
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            var postulado = vacante.UsuarioVacantes
                .FirstOrDefault(p => p.UsuarioId == usuarioId);

            postulado.Estado = "Rechazado";

            await _context.SaveChangesAsync();
        }


        public async Task CrearPostulacion(Postulante usuarioVacante)
        {
            _context.UsuarioVacantes.Add(usuarioVacante);
            await _context.SaveChangesAsync();
        }

        public async Task BorrarVacante(int vacanteId)
        {
            var postulados = _context.UsuarioVacantes
                .Where(uv => uv.VacanteId == vacanteId);

            _context.UsuarioVacantes.RemoveRange(postulados);

            var entity = new Vacante { Id = vacanteId };
            _context.Vacantes.Attach(entity);
            _context.Vacantes.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsuarioPuedePostular(int vacanteId, int? usuarioId)
        {
            return await _context.UsuarioVacantes
                .AnyAsync(p => p.UsuarioId == usuarioId && p.VacanteId == vacanteId);
        }

    }
}
