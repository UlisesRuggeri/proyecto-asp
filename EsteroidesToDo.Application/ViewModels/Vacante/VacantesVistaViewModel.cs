using EsteroidesToDo.Application.DTOs.VacanteDtos;


namespace EsteroidesToDo.Application.ViewModels;

    public class VacantesVistaViewModel
    {
        public bool EsDuenio { get; set; }
        public required List<VacanteInfoDto> Vacantes { get; set; }
    }


