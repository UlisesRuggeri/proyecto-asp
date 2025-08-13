
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IVacanteRepository
    {

        Task<bool> PuedeCrearVacanteAsync(int usuarioId);

        Task AgregarVacanteAsync(Vacante vacante);

        Task<List<Vacante>> ObtenerTodasAsync();

        Task<List<Vacante>> ObtenerPorEmpresaAsync(int usuarioId);

        Task ActualizarEstadoAsync(int vacanteId, string nuevoEstado);

        Task MarcarPostuladoComoAceptadoAsync(int vacanteId, int usuarioId);

        Task MarcarPostuladoComoRechazadoAsync(int vacanteId, int usuarioId);
    }
}
