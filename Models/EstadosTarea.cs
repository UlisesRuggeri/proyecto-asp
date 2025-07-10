using System.ComponentModel.DataAnnotations;

namespace EsteroidesToDo.Models
{
    public class EstadosTarea
    {
        public int Id { get; set; }
        [Required]
        public string? Estado { get; set; } 
    }
}
