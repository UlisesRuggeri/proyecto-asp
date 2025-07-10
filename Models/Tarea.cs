using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsteroidesToDo.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        public int EstadosTareaId { get; set; }

        public int ProyectoId { get; set; }

        public int UsuarioAsignadoId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioAsignadorId { get; set; }

        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [ForeignKey(nameof(ProyectoId))]
        public Proyecto? Proyecto { get; set; }

        [ForeignKey(nameof(EstadosTareaId))]
        public EstadosTarea? Estado { get; set; }

        [ForeignKey(nameof(UsuarioAsignadoId))]
        public Usuario? UsuarioAsignado { get; set; }

        [ForeignKey(nameof(UsuarioCreadorId))]
        public Usuario? UsuarioCreador { get; set; } 

        [ForeignKey(nameof(UsuarioAsignadorId))]
        public Usuario? UsuarioAsignador { get; set; }

        public ICollection<ConversacionesTarea> ConversacionesTareas { get; set; } = new List<ConversacionesTarea>();

    }
}
