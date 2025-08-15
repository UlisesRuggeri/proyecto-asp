
using EsteroidesToDo.Domain.Filters;
using EsteroidesToDo.Domain.Pagination;
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IVacanteRepository
    {

        Task<bool> PuedeCrearVacanteAsync(int? usuarioId);

        Task AgregarVacanteAsync(Vacante vacante);

        Task<List<Vacante>> ObtenerPorEmpresaAsync(int usuarioId);
        Task MarcarPostuladoComoAceptadoAsync(int vacanteId, int usuarioId);

        Task MarcarPostuladoComoRechazadoAsync(int vacanteId, int usuarioId);

        Task CrearPostulacion(Postulante usuarioVacante);

        Task BorrarVacante(int VacanteId);

        Task<bool> UsuarioPuedePostular(int VacanteId, int? UsuarioId);

        Task<PagedResult<Vacante>> GetVacantesAsync(
            VacanteFilter filter,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
