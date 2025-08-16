using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Models;
using EsteroidesToDo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly EsteroidesToDoDbContext _context;

        public EmpresaRepository(EsteroidesToDoDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────
        // OBTENER
        // ─────────────────────────────────────
        public async Task<Empresa?> ObtenerPorIdDuenioAsync(int idDuenio)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.IdDuenio == idDuenio);
        }

        public async Task<Empresa?> ObtenerEmpresaPorIdUsuario(int userId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
            return await _context.Empresas.FirstOrDefaultAsync(e => e.Id == usuario.EmpresaId);
        }

        // ─────────────────────────────────────
        // CREAR
        // ─────────────────────────────────────
        public async Task CrearEmpresa(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }
    }
}
