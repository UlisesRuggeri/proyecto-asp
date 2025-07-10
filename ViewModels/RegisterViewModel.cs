using System.ComponentModel.DataAnnotations;

namespace EsteroidesToDo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "tenes q poner una mas grande")]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string ContraseniaHash { get; set; }
    }
}