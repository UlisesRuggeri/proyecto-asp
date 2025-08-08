using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EsteroidesToDo.Application.ViewModels.EmpresaViewModels
{
    public class EmpresaViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "tenes q poner una mas grande")]
        public string Nombre { get; set; }

        [Required, NotNull]
        public string Descripcion { get; set; }


    }
}