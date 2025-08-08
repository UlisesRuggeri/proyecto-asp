using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsteroidesToDo.Models
{
    public class ConversacionesTarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TareaId { get; set; }
        public int UsuarioId {get;set;}
        [Required]
        public string? Mensaje { get; set; }

        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [ForeignKey("TareaId")]
        public Tarea? Tareas { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuarios { get; set; }
    }
}
