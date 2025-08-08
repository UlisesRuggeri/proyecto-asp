using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EsteroidesToDo.Models
{
    public class UsuarioProyectoRol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [Required]
        public string Rol { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }
    }
}
