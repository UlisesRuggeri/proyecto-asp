using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsteroidesToDo.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public ICollection<Proyecto> Proyectos { get; set; } = null!;

    }
}
