using EsteroidesToDo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notificacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int IdEmpresa { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(500)]
    public string Contenido { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [ForeignKey("IdEmpresa")]
    public Empresa Empresa { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario Usuario { get; set; }
}
