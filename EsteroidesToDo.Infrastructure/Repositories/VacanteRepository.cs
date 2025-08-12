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
        public async Task<bool> UsuarioPuedeCrearVacante(int usuarioId)
        {
            return await _context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.IdDuenio == usuarioId);
        }

        public async Task CrearVacante(Vacante vacante)
        {
            _context.Vacantes.Add(vacante);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vacante>> ObtenerTodasLasVacantes()
        {
            return await _context.Vacantes
                .Include(v => v.Empresa)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Vacante>> ObtenerTodasLasVacantesDeUnaEmpresa(int usuarioId)
        {
            return await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .Where(v => v.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task CambiarEstadoVacante(int vacanteId, string nuevoEstado)
        {
            var vacante = await _context.Vacantes.FirstOrDefaultAsync(v => v.Id == vacanteId);
            if (vacante == null) return;
            vacante.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
        }

        public async Task AceptarPostulado(int vacanteId, int usuarioId)
        {

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            if (vacante == null)
                throw new Exception("Vacante no encontrada");

            var postulado = vacante.UsuarioVacantes.FirstOrDefault(p => p.UsuarioId == usuarioId);

            if (postulado == null)
                throw new Exception("Postulado no encontrado");

            postulado.Estado = "Aceptado";
            if(usuario.EmpresaId != null)
            {
                throw new Exception("Usuario ya pertenece a una empresa");
            }
            usuario.EmpresaId = vacante.EmpresaId;
            await _context.SaveChangesAsync();
        }

        public async Task RechazarPostulado(int usuarioId, int vacanteId)
        {

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            var vacante = await _context.Vacantes
                .Include(v => v.UsuarioVacantes)
                .FirstOrDefaultAsync(v => v.Id == vacanteId);

            if (vacante == null)
                throw new Exception("Vacante no encontrada");

            var postulado = vacante.UsuarioVacantes.FirstOrDefault(p => p.UsuarioId == usuarioId);

            if (postulado == null)
                throw new Exception("Postulado no encontrado");

            postulado.Estado = "Rechazado";
            if (usuario.EmpresaId != null)
            {
                throw new Exception("Usuario ya pertenece a una empresa");
            }
            await _context.SaveChangesAsync();
        }

    }
}
