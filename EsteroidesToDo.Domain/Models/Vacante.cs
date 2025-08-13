using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsteroidesToDo.Models
{
    public class Vacante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        [Required]
        public string Estado { get; set; } = "Activa";

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<UsuarioVacante> Postulaciones { get; set; } = new List<UsuarioVacante>();
        public ICollection<UsuarioVacante> UsuarioVacantes { get; set; } = new List<UsuarioVacante>();



    }
}
