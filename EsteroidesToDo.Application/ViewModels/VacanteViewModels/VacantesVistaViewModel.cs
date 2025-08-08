using EsteroidesToDo.Application.DTOs.VacanteDtos;

namespace EsteroidesToDo.Application.ViewModels.VacanteViewModel
{
    public class VacantesVistaViewModel
    {
        public bool EsDuenio { get; set; }
        public List<VacanteInfoDto> Vacantes { get; set; }
    }

}
