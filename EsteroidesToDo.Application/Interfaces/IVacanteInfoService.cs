

using EsteroidesToDo.Application.DTOs.VacanteDtos;

namespace EsteroidesToDo.Application.Interfaces
{

    public interface IVacanteInfoService
    {
        //no representa una abstraccion de negocio, sino de como se accede a la informacion 
        Task<List<VacanteInfoDto>> ObtenerTodasLasVacantes();
    }
}
