

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

    }
}
