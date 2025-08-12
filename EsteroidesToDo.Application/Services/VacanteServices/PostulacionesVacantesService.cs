

using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Application.Services.VacanteServices
{
    public class PostulacionesVacantesService
    {
        private readonly IVacanteRepository _repo;

        public PostulacionesVacantesService(IVacanteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Vacante>> ObtenerTodasLasVacantesDeUnaEmpresa(int usuarioId)
        {
            if (!await _repo.UsuarioPuedeCrearVacante(usuarioId))
            {
                return new List<Vacante>(); 
            }

            return await _repo.ObtenerTodasLasVacantesDeUnaEmpresa(usuarioId);
        }

        public async Task CambiarEstadoVacante(int vacanteId,string nuevoEstado)
        {
            var estadosPermitidos = new[] { "Activa", "Cerrada" };
            if (!estadosPermitidos.Contains(nuevoEstado))
                throw new ArgumentException("Estado no permitido.");

            await _repo.CambiarEstadoVacante(vacanteId, nuevoEstado);
        }

        public async Task AceptarPostulado(int vacanteId, int usuarioId)
        {
            await _repo.AceptarPostulado(vacanteId, usuarioId);
        }

        public async Task RechazarPostulado(int usuarioId, int vacanteId)
        {
            await _repo.RechazarPostulado(int usuarioId, int vacanteId);
        }

    }
}
