using System.ComponentModel.DataAnnotations;

namespace EsteroidesToDo.Application.ViewModels.UsuarioViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
