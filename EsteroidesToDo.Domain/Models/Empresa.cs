using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EsteroidesToDo.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, NotNull]
        public string Nombre { get; set; }

        [Required, NotNull]
        public string Descripcion { get; set; }

        public int IdDuenio { get; set; }
        [ForeignKey("IdDuenio")]
        public Usuario Duenio { get; set; }


        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Today);
            


        public ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
