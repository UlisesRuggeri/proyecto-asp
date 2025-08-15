using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Infrastructure.Data;
using EsteroidesToDo.Models;
using Microsoft.EntityFrameworkCore;
using EsteroidesToDo.Domain.Pagination;
using EsteroidesToDo.Domain.Filters;

namespace EseroidesToDo.Infrastructure.Repositories
{
    internal class VacanteRepository : IVacanteRepository
    {
        private readonly EsteroidesToDoDbContext _context;

        public VacanteRepository(EsteroidesToDoDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────
        // VALIDACIONES
        // ─────────────────────────────────────
        public async Task<bool> PuedeCrearVacanteAsync(int? usuarioId)
        {
            return await _context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.IdDuenio == usuarioId);
        }

        public async Task<bool> UsuarioPuedePostular(int vacanteId, int? usuarioId)
        {
            return await _context.UsuarioVacantes
                .AnyAsync(p => p.UsuarioId == usuarioId && p.VacanteId == vacanteId);
        }

        // ─────────────────────────────────────
        // CREAR / AGREGAR
        // ─────────────────────────────────────
        public async Task AgregarVacanteAsync(Vacante vacante)
        {
            _context.Vacantes.Add(vacante);
            await _context.SaveChangesAsync();
        }

        public async Task CrearPostulacion(Postulante usuarioVacante)
        {
            _context.UsuarioVacantes.Add(usuarioVacante);
            await _context.SaveChangesAsync();
        }

        // ─────────────────────────────────────
        // OBTENER
        // ─────────────────────────────────────

        public async Task<PagedResult<Vacante>> GetVacantesAsync(VacanteFilter filter, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Vacantes
                .Include(v => v.Empresa)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                query = query.Where(v =>
                    v.Titulo.Contains(filter.SearchTerm) ||
                    v.Empresa.Nombre.Contains(filter.SearchTerm) ||
                    v.Estado.Contains(filter.SearchTerm) ||
                    v.Descripcion.Contains(filter.SearchTerm)) ;
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(v => v.FechaCreacion)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Vacante>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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

        // ─────────────────────────────────────
        // ACTUALIZAR
        // ─────────────────────────────────────
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

        // ─────────────────────────────────────
        // ELIMINAR
        // ─────────────────────────────────────
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

    }
}
