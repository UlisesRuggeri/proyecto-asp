using EsteroidesToDo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, NotNull]
    [MinLength(5)]
    public string Nombre { get; set; }

    [Required, NotNull]
    [MinLength(7)]
    public string Email { get; set; }

    [Required, NotNull]
    [MinLength(6)]
    public string ContraseniaHash { get; set; }

    public int? EmpresaId { get; set; }

    [ForeignKey("EmpresaId")]
    public Empresa? Empresa { get; set; }


    public ICollection<UsuarioVacante> UsuarioVacantes { get; set; } = new List<UsuarioVacante>();

    public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Today);

}
