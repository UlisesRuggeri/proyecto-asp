
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IVacanteRepository
    {
        Task<bool> UsuarioPuedeCrearVacante(int usuarioId);
        Task CrearVacante(Vacante vacante);

        Task<List<Vacante>> ObtenerTodasLasVacantes();

        Task<List<Vacante>> ObtenerTodasLasVacantesDeUnaEmpresa(int usuarioId);

        Task CambiarEstadoVacante(int vacanteId, string nuevoEstado);

        Task AceptarPostulado(int vacanteId, int usuarioId);

        Task RechazarPostulado(int usuarioId, int vacanteId);
    }
}
