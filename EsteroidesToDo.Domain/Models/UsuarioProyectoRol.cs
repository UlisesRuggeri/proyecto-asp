using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EsteroidesToDo.Models
{
    public class UsuarioProyectoRol
    {
        //los siguientes 2 de abajo son la llave compuesta
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
