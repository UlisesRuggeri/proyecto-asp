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
                .Where(v => v.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
