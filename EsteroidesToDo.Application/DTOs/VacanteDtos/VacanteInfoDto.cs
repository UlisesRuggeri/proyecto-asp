
using EsteroidesToDo.Application.Common;

namespace EsteroidesToDo.Application.DTOs.VacanteDtos
{
    public class VacanteInfoDto
    {
        public int VacanteId { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public string? EmpresaNombre { get; set; }
        public bool PuedePostular { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
