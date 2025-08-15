
using System.ComponentModel.DataAnnotations;

namespace EsteroidesToDo.Application.ViewModels
{
    public class PostuladoViewModel
    {

        //usuarioId y vacanteId es una llave compuesta
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int VacanteId { get; set; }
        [Required, MaxLength(255)]
        public string PropuestaTexto { get; set; }
        public string Estado { get; set; } = "activo";
    }
}
