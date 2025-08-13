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
        public async Task<bool> PuedeCrearVacanteAsync(int usuarioId)
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
            return await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .Where(v => v.UsuarioVacantes.Any(uv => uv.UsuarioId == usuarioId))
                .AsNoTracking()
                .ToListAsync();

        }
        public async Task ActualizarEstadoAsync(int vacanteId, string nuevoEstado)
        {
            var vacante = await _context.Vacantes.FirstOrDefaultAsync(v => v.Id == vacanteId);
            if (vacante == null) return;
            vacante.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
        }

        public async Task MarcarPostuladoComoAceptadoAsync(int vacanteId, int usuarioId)
        {

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            var postulado = vacante.UsuarioVacantes.FirstOrDefault(p => p.UsuarioId == usuarioId);

            postulado.Estado = "Aceptado";
           
            usuario.EmpresaId = vacante.EmpresaId;
            await _context.SaveChangesAsync();
        }

        public async Task MarcarPostuladoComoRechazadoAsync(int vacanteId, int usuarioId)
        {

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            var postulado = vacante.UsuarioVacantes.FirstOrDefault(p => p.UsuarioId == usuarioId);

            postulado.Estado = "Rechazado";

            await _context.SaveChangesAsync();
        }

    }
}
