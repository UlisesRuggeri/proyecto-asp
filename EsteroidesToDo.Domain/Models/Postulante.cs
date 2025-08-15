using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsteroidesToDo.Models
{
    public class Postulante
    {

        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public int VacanteId { get; set; }
        [ForeignKey("VacanteId")]
        public Vacante Vacante { get; set; }

        [Required]
        public string PropuestaTexto { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaPostulacion { get; set; } = DateTime.UtcNow;

    }
}
