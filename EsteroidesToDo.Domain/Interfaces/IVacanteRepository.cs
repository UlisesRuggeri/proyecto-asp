
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IVacanteRepository
    {
        Task<bool> UsuarioPuedeCrearVacante(int usuarioId);
        Task CrearVacante(Vacante vacante);

        Task<List<Vacante>> ObtenerTodasLasVacantes(); 
    }
}
