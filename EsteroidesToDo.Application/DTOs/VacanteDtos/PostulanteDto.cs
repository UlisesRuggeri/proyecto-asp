
using System.ComponentModel.DataAnnotations;

namespace EsteroidesToDo.Application.DTOs.VacanteDtos
{
    public class PostulanteDto
    {
        
        public int UsuarioId { get; set; }
        public int VacanteId { get; set; }
        [Required, MaxLength(255)]
        public string PropuestaTexto { get; set; }
        public string Estado { get; set; }
    }
}
